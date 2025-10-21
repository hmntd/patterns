<?php

namespace App\Interfaces;

use App\Models\Category;
use App\Models\QuizAttempt;
use Illuminate\Support\Collection;

interface AttemptRepositoryInterface
{
    public function index(): Collection;
    public function create(Category $category, array $data): QuizAttempt;
    public function show(QuizAttempt $attempt): QuizAttempt;
    public function update(QuizAttempt $attempt, array $data): QuizAttempt;
    public function delete(QuizAttempt $attempt): bool;
}
