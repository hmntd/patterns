<?php

namespace App\Repositories;

use App\Interfaces\AttemptRepositoryInterface;
use App\Models\AttemptAnswers;
use App\Models\Category;
use App\Models\QuizAttempt;
use App\Services\QuizScoreManager;
use Illuminate\Support\Collection;
use Illuminate\Support\Facades\DB;

class AttemptRepository implements AttemptRepositoryInterface
{
    public function index(): Collection
    {
        return QuizAttempt::all();
    }

    public function create(Category $category, array $data): QuizAttempt
    {
        return DB::transaction(function () use ($category, $data) {
            $user = auth()->user();

            $questions = $category->questions()->with('answers')->get();
            $totalQuestions = $questions->count();
            $score = QuizScoreManager::getInstance()->calculateScore($data['answers'] ?? [], $questions);

            $attempt = QuizAttempt::where('user_id', $user->id)
                ->where('category_id', $category->id)
                ->first();

            if ($attempt) {
                AttemptAnswers::where('quiz_attempt_id', $attempt->id)->delete();

                $attempt->update([
                    'score' => $score,
                    'total_questions' => $totalQuestions,
                    'updated_at' => now(),
                ]);
            } else {
                $attempt = QuizAttempt::create([
                    'category_id' => $category->id,
                    'user_id' => $user->id,
                    'score' => $score,
                    'total_questions' => $totalQuestions,
                ]);
            }

            $insertData = [];
            $answersData = $data['answers'] ?? [];

            foreach ($answersData as $questionId => $answerIds) {
                $answerIds = is_array($answerIds) ? $answerIds : [$answerIds];

                foreach ($answerIds as $answerId) {
                    $insertData[] = [
                        'quiz_attempt_id' => $attempt->id,
                        'question_id' => $questionId,
                        'answer_id' => $answerId,
                        'created_at' => now(),
                        'updated_at' => now(),
                    ];
                }
            }

            if (!empty($insertData)) {
                AttemptAnswers::insert($insertData);
            }

            return $attempt;
        });
    }

    public function show(QuizAttempt $attempt): QuizAttempt
    {
        return $attempt;
    }

    public function update(QuizAttempt $attempt, array $data): QuizAttempt
    {
        $attempt->update($data);
        return $attempt;
    }

    public function delete(QuizAttempt $attempt): bool
    {
        return $attempt->delete();
    }
}
