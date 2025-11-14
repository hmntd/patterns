<?php

namespace App\Repositories;

use App\Interfaces\AnswerRepositoryInterface;
use App\Models\Answer;
use Illuminate\Support\Collection;

class AnswerRepository implements AnswerRepositoryInterface
{
    public function index(): Collection
    {
        return Answer::all();
    }

    public function create(array $data): Answer
    {
        return Answer::create($data);
    }

    public function show(Answer $answer): Answer
    {
        return $answer;
    }

    public function update(Answer $answer, array $data): Answer
    {
        $answer->update($data);

        return $answer;
    }

    public function delete(Answer $answer): bool
    {
        return $answer->delete();
    }
}
