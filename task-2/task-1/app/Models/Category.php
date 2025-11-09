<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Casts\Attribute;
use Illuminate\Support\Facades\Auth;

class Category extends Model
{
    protected $table = 'categories';

    protected $fillable = [
        'id',
        'name',
        'description',
        'created_at',
    ];

    protected $hidden = [
        'updated_at',
    ];

    protected $appends = ['has_passed', 'result'];

    protected function hasPassed(): Attribute
    {
        return Attribute::make(
            get: function () {
                $user = Auth::user();
                if (!$user) {
                    return false;
                }

                return $this->attempts()
                    ->where('user_id', $user->id)
                    ->exists();
            },
        );
    }

    protected function result(): Attribute
    {
        return Attribute::make(
            get: function () {
                return $this->attempts()
                    ->where('user_id', auth()->user()->id)
                    ->first();
            },
        );
    }

    public function questions(): HasMany
    {
        return $this->hasMany(Question::class);
    }

    public function attempts(): HasMany
    {
        return $this->hasMany(QuizAttempt::class, 'category_id', 'id');
    }
}
