<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Answer extends Model
{
    protected $table = 'answers';

    protected $fillable = [
        'id',
        'question_id',
        'answer',
        'is_correct',
        'created_by',
    ];

    protected $hidden = [
        'question_id',
        // 'is_correct',
        'created_at',
        'updated_at',
        'created_by',
    ];

    public function question(): BelongsTo
    {
        return $this->belongsTo(Question::class);
    }
}
