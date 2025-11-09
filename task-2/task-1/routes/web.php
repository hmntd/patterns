<?php

use Illuminate\Support\Facades\Route;
use Inertia\Inertia;

Route::get('/', function () {
    return redirect()->route('login');
})->name('home');

Route::get('dashboard', function () {
    return Inertia::render('Dashboard');
})->middleware(['auth', 'verified'])->name('dashboard');

Route::get('/categories/{id}/leaderboard', function ($id) {
    return Inertia::render('Categories/Leaderboard', [
        'categoryId' => (int) $id,
    ]);
})->middleware(['auth', 'verified'])->name('leaderboard');

Route::get('/categories/{id}', function ($id) {
    return Inertia::render('Categories/CategoryQuizPage', [
        'categoryId' => (int) $id,
    ]);
})->middleware(['auth', 'verified'])->name('category');

require __DIR__.'/settings.php';
require __DIR__.'/auth.php';
