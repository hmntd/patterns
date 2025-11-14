<?php

namespace App\Interfaces;

use App\Models\Question;
use Illuminate\Support\Collection;

interface QuestionRepositoryInterface
{
    public function index(): Collection;
    public function create(array $data): Question;
    public function show(Question $question): Question;
    public function update(Question $question, array $data): Question;
    public function delete(Question $question): bool;
    public function clearAnswers(Question $question): bool;
}
