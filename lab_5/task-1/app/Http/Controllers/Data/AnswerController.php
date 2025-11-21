<?php

namespace App\Http\Controllers\Data;

use App\Http\Controllers\Controller;
use App\Models\Answer;
use App\Repositories\AnswerRepository;
use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;
use Illuminate\Http\Response;

class AnswerController extends Controller
{
    public function __construct(
        private AnswerRepository $answerRepository
    ) {}

    public function index(): JsonResponse
    {
        try {
            $answers = $this->answerRepository->index();

            return response()->json([
                'success' => true,
                'answers' => $answers
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function show(Answer $answer): JsonResponse
    {
        try {
            $answer = $this->answerRepository->show($answer);

            return response()->json([
                'success' => true,
                'answer' => $answer
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
            $answer = $this->answerRepository->create($request->all());

            return response()->json([
                'success' => true,
                'answer' => $answer
            ], Response::HTTP_CREATED);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function update(Request $request, Answer $answer): JsonResponse
    {
        try {
            $answer = $this->answerRepository->update($answer, $request->all());

            return response()->json([
                'success' => true,
                'answer' => $answer
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }

    public function destroy(Answer $answer): JsonResponse
    {
        try {
            $this->answerRepository->delete($answer);

            return response()->json([
                'success' => true
            ], Response::HTTP_OK);
        } catch (\Exception $e) {
            return response()->json([
                'success' => false,
                'message' => $e->getMessage()
            ], Response::HTTP_INTERNAL_SERVER_ERROR);
        }
    }
}
