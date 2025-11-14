<?php

namespace App\Http\Controllers;

use App\Models\Question;
use App\Repositories\QuestionRepository;
use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;
use Illuminate\Http\Response;

class QuestionController extends Controller
{
    public function __construct(
        private QuestionRepository $questionRepository
    ) {}

    public function index(): JsonResponse
    {
        try {
            $questions = $this->questionRepository->index();

            return response()->json([
                'success' => true,
                'questions' => $questions
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function show(Question $question): JsonResponse
    {
        try {
            $question = $this->questionRepository->show($question);

            return response()->json([
                'success' => true,
                'question' => $question
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
            $question = $this->questionRepository->create($request->all());

            return response()->json([
                'success' => true,
                'question' => $question
            ], Response::HTTP_CREATED);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function update(Request $request, Question $question): JsonResponse
    {
        try {
            $question = $this->questionRepository->update($question, $request->all());

            return response()->json([
                'success' => true,
                'question' => $question
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function destroy(Question $question): JsonResponse
    {
        try {
            $result = $this->questionRepository->delete($question);

            return response()->json([
                'success' => $result,
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'error' => $e->getMessage(),
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function clearAnswers(Question $question): JsonResponse
    {
        try {
            $result = $this->questionRepository->clearAnswers($question);

            return response()->json([
                'success' => $result,
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'error' => $e->getMessage(),
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }
}
