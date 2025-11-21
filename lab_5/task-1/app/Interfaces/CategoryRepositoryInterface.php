<?php

namespace App\Interfaces;

use App\Models\Category;
use Illuminate\Support\Collection;

interface CategoryRepositoryInterface
{
    public function index(): Collection;
    public function create(array $data): Category;
    public function show(Category $category): Category;
    public function update(Category $category, array $data): Category;
    public function delete(Category $category): bool;
    public function getTop20(Category $category): Collection;
}
