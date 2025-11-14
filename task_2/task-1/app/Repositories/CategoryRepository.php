<?php

namespace App\Repositories;

use App\Interfaces\CategoryRepositoryInterface;
use App\Models\Category;
use App\Models\QuizAttempt;
use Illuminate\Support\Collection;

class CategoryRepository implements CategoryRepositoryInterface
{
    public function index(): Collection
    {
        return Category::with('questions.answers')->get();
    }

    public function create(array $data): Category
    {
        return Category::create($data);
    }

    public function show(Category $category): Category
    {
        return $category->load('questions.answers');
    }

    public function update(Category $category, array $data): Category
    {
        $category->update($data);
        return $category;
    }

    public function delete(Category $category): bool
    {
        return $category->delete();
    }

    public function getTop20(Category $category): Collection
    {
        return $category->attempts()
            ->orderByDesc('score')
            ->orderBy('created_at')
            ->limit(20)
            ->with('user')
            ->get();
    }
}
