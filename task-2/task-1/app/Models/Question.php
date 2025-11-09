<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Question extends Model
{
    protected $table = 'questions';

    protected $fillable = [
        'id',
        'category_id',
        'question',
        'is_multiple_choice',
        'created_by',
    ];

    protected $hidden = [
        'category_id',
        'created_at',
        'updated_at',
        'created_by',
    ];

    public function category(): BelongsTo
    {
        return $this->belongsTo(Category::class);
    }

    public function answers(): HasMany
    {
        return $this->hasMany(Answer::class);
    }
}
