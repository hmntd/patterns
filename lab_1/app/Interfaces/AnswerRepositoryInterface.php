<?php

namespace App\Interfaces;

use App\Models\Answer;
use Illuminate\Support\Collection;

interface AnswerRepositoryInterface
{
    public function index(): Collection;
    public function create(array $data): Answer;
    public function show(Answer $answer): Answer;
    public function update(Answer $answer, array $data): Answer;
    public function delete(Answer $answer): bool;
}
