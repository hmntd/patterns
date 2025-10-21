<?php

namespace App\Repositories;

use App\Interfaces\QuestionRepositoryInterface;
use App\Models\Question;
use Illuminate\Support\Collection;

class QuestionRepository implements QuestionRepositoryInterface
{
    public function index(): Collection
    {
        return Question::all();
    }

    public function create(array $data): Question
    {
        return Question::create($data);
    }

    public function show(Question $question): Question
    {
        return $question->with('answers')->first();
    }

    public function update(Question $question, array $data): Question
    {
        $question->update($data);

        return $question;
    }

    public function delete(Question $question): bool
    {
        return $question->delete();
    }

    public function clearAnswers(Question $question): bool
    {
        return $question->answers()->delete();
    }
}
