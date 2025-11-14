<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class AttemptAnswers extends Model
{
    protected $table = 'quiz_attempt_answers';

    protected $fillable = [
        'quiz_attempt_id',
        'question_id',
        'answer_id',
    ];

    protected $hidden = [
        'quiz_attempt_id',
        'question_id',
        'answer_id',
        'created_at',
        'updated_at',
    ];

    public function attempt(): BelongsTo
    {
        return $this->belongsTo(User::class);
    }

    public function question(): BelongsTo
    {
        return $this->belongsTo(Question::class);
    }

    public function answer(): BelongsTo
    {
        return $this->belongsTo(Answer::class);
    }
}
