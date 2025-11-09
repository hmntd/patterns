<?php

namespace App\Services;

class QuizScoreManager
{
    private static ?QuizScoreManager $instance = null;

    private function __construct() {}

    public static function getInstance(): QuizScoreManager
    {
        if (self::$instance === null) {
            self::$instance = new QuizScoreManager();
        }
        return self::$instance;
    }

    /**
     * Calculate the score of a quiz based on user answers
     *
     * @param array $userAnswers The answers provided by the user
     * @param mixed $questions The questions of the quiz
     *
     * @return float The score calculated
     */
    public function calculateScore(array $userAnswers, $questions): float
    {
        $score = 0;

        foreach ($questions as $question) {
            $selectedAnswers = collect($userAnswers[$question->id] ?? []);

            if ($selectedAnswers->isEmpty()) continue;

            $correctAnswers = $question->answers->where('is_correct', true)->pluck('id');

            $allCorrectSelected = $correctAnswers->diff($selectedAnswers)->isEmpty();
            $onlyCorrectSelected = $selectedAnswers->diff($correctAnswers)->isEmpty();

            if ($question->is_multiple_choice) {
                if ($allCorrectSelected && $onlyCorrectSelected) {
                    $score += 1;
                } elseif ($onlyCorrectSelected) {
                    $score += 0.5;
                }
            } else {
                if ($onlyCorrectSelected && $allCorrectSelected) {
                    $score += 1;
                }
            }
        }

        return $score;
    }
}
