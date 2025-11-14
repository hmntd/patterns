<?php
namespace App\Builders;

use App\Models\Category;
use App\Models\Question;
use App\Models\Answer;

class QuizBuilder
{
    private Category $category;
    private array $questions = [];

    public function setCategory(string $name, string $description): self
    {
        $this->category = Category::create([
            'name' => $name,
            'description' => $description
        ]);
        return $this;
    }

    public function addQuestion(string $text, bool $isMultiple, array $answers): self
    {
        $question = Question::create([
            'category_id' => $this->category->id,
            'question' => $text,
            'is_multiple_choice' => $isMultiple
        ]);

        foreach ($answers as $ans) {
            Answer::create([
                'question_id' => $question->id,
                'answer' => $ans['text'],
                'is_correct' => $ans['is_correct']
            ]);
        }

        $this->questions[] = $question;
        return $this;
    }

    public function build(): Category
    {
        return $this->category;
    }
}
