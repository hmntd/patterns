<?php

use App\Http\Controllers\CategoryController;
use App\Http\Controllers\Data\AnswerController;
use App\Http\Controllers\Data\AttemptController;
use App\Http\Controllers\QuestionController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

Route::get('/user', function (Request $request) {
    return $request->user();
})->middleware('auth:sanctum');

Route::get('/create-token', function () {
    auth()->user()->tokens()->delete();
    $token = auth()->user()->createToken('api_token')->plainTextToken;
    return response()->json([
        'token' => $token,
    ], 200);
})->middleware(['web', 'auth']);

Route::middleware('auth:sanctum')->group(function () {
    // categories
    Route::apiResource('categories', CategoryController::class); // index, store, show, update, destroy
    Route::prefix('categories')->group(function () {
        Route::get('/{category}/top', [CategoryController::class, 'getTop20']);
    });

    // questions
    Route::apiResource('questions', QuestionController::class);
    Route::delete('/categories/{category}/questions/clear', [QuestionController::class, 'clearAnswers']);

    // answers
    Route::apiResource('answers', AnswerController::class);

    Route::apiResource('attempts', AttemptController::class);
});
