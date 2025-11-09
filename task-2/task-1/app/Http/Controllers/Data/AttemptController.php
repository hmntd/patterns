<?php

namespace App\Http\Controllers\Data;

use App\Http\Controllers\Controller;
use App\Models\Category;
use App\Models\QuizAttempt;
use App\Repositories\AttemptRepository;
use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;
use Illuminate\Http\Response;

class AttemptController extends Controller
{
    public function __construct(
        private AttemptRepository $attemptRepository
    ) {}

    public function index(): JsonResponse
    {
        try {
            $attempts = $this->attemptRepository->index();

            return response()->json([
                'success' => true,
                'attempts' => $attempts
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function store(Request $request): JsonResponse
    {
        try {
            $category = Category::findOrFail($request->category_id);

            return response()->json([
                'success' => true,
                'attempt' => $this->attemptRepository->create($category, $request->all())
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function show(QuizAttempt $attempt): JsonResponse
    {
        try {
            return response()->json([
                'success' => true,
                'attempt' => $this->attemptRepository->show($attempt)
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function update(QuizAttempt $attempt, Request $request): JsonResponse
    {
        try {
            return response()->json([
                'success' => true,
                'attempt' => $this->attemptRepository->update($attempt, $request->all())
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function destroy(QuizAttempt $attempt): JsonResponse
    {
        try {
            return response()->json([
                'success' => true,
                'attempt' => $this->attemptRepository->delete($attempt)
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }
}
