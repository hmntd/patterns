<?php

namespace App\Services;

use App\Models\Question;

class QuestionFactory
{
    /**
     * Creates a new question model instance.
     *
     * @param string $type - the type of question, either 'single' or 'multiple'.
     * @param array $data - the data for the question.
     * @return Question - a new question model instance.
     */
    public static function create(string $type, array $data): Question
    {
        $question = new Question();
        $question->question = $data['question'] ?? '';
        $question->category_id = $data['category_id'] ?? null;
        $question->is_multiple_choice = $type === 'multiple';
        $question->created_by = auth()->user()->id;
        $question->save();

        return $question;
    }
}
