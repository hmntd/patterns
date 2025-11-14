<?php

namespace App\Http\Controllers;

use App\Models\Category;
use App\Repositories\CategoryRepository;
use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;
use Illuminate\Http\Response;
use Illuminate\Support\Facades\Log;

class CategoryController extends Controller
{
    public function __construct(
        private CategoryRepository $categoryRepository
    ) {}

    public function index(): JsonResponse
    {
        try {
            $categories = $this->categoryRepository->index();

            return response()->json([
                'success' => true,
                'categories' => $categories,
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'error' => $e->getMessage(),
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function store(Request $request): JsonResponse
    {
        try {
            $category = $this->categoryRepository->create($request->all());
            Log::info('category', [
                'category' => $category
            ]);
            return response()->json([
                'success' => true,
                'category' => $category,
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'error' => $e->getMessage(),
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function show(Category $category): JsonResponse
    {
        try {
            $category = $this->categoryRepository->show($category);

            return response()->json([
                'success' => true,
                'category' => $category,
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'error' => $e->getMessage(),
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function update(Request $request, Category $category): JsonResponse
    {
        try {
            $category = $this->categoryRepository->update($category, $request->all());

            return response()->json([
                'success' => true,
                'category' => $category,
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'error' => $e->getMessage(),
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function destroy(Category $category): JsonResponse
    {
        try {
            $this->categoryRepository->delete($category);

            return response()->json([
                'success' => true,
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'error' => $e->getMessage(),
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function getTop20(Category $category): JsonResponse
    {
        try {
            $attempts = $this->categoryRepository->getTop20($category);

            return response()->json([
                'success' => true,
                'leaders' => $attempts,
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'error' => $e->getMessage(),
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }
}
