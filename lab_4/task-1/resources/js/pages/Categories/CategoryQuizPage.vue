<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { router, usePage } from '@inertiajs/vue3'
import Card from '@/components/ui/card/Card.vue';
import Spinner from '@/components/ui/spinner/Spinner.vue';
import Checkbox from '@/components/ui/checkbox/Checkbox.vue';
import Button from '@/components/ui/button/Button.vue';

const props = defineProps<{ categoryId: number }>()
const { categoryId } = props

const page = usePage();
const loading = ref(true);
const submitting = ref(false);
const category = ref<any>(null);
const currentQuestionIndex = ref(0);
const answers = ref<Record<number, number[]>>({});
const result = ref<any>(null);

const fetchCategory = async () => {
    loading.value = true
    const response = await fetch(`/api/categories/${categoryId}`, {
        headers: {
            Accept: 'application/json',
            Authorization: 'Bearer ' + localStorage.getItem('token'),
        }
    });
    const data = await response.json();
    category.value = data.category
    loading.value = false
}

const toggleAnswer = (questionId: number, answerId: number, multi: boolean) => {
    if (!answers.value[questionId]) answers.value[questionId] = [];

    if (multi) {
        const index = answers.value[questionId].indexOf(answerId);
        if (index >= 0) answers.value[questionId].splice(index, 1);
        else answers.value[questionId].push(answerId);
    } else {
        answers.value[questionId] = [answerId];
    }
}

const submitQuiz = async () => {
    if (!category.value) return
    const total = category.value.questions.length
    const answered = Object.keys(answers.value).length

    if (answered < total) {
        alert('Please answer all questions before submitting!')
        return
    }

    submitting.value = true
    const res = await fetch('/api/attempts', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
        },
        body: JSON.stringify({
            category_id: category.value.id,
            user_id: page.props.auth.user.id,
            answers: answers.value
        })
    })
    const data = await res.json()
    submitting.value = false
    result.value = data
}

const nextQuestion = () => {
    if (currentQuestionIndex.value < category.value.questions.length - 1) {
        currentQuestionIndex.value++
    }
}

const prevQuestion = () => {
    if (currentQuestionIndex.value > 0) {
        currentQuestionIndex.value--
    }
}

onMounted(async () => {
    await fetchCategory();
});
</script>

<template>
    <div class="max-w-3xl mx-auto py-10">
        <Spinner v-if="loading" class="mx-auto" />
        <template v-else>
            <h1 class="text-2xl font-bold mb-4 text-center">
                {{ category.name }} Quiz
            </h1>

            <div v-if="!result">
                <Card class="p-6">
                    <div v-if="category.questions.length > 0">
                        <div class="mb-4">
                            <p class="text-lg font-semibold mb-2">
                                Question {{ currentQuestionIndex + 1 }} of {{ category.questions.length }}
                            </p>
                            <p>{{ category.questions[currentQuestionIndex].question }}</p>
                        </div>

                        <div class="space-y-2">
                            <div v-for="ans in category.questions[currentQuestionIndex].answers" :key="ans.id"
                                class="flex items-center gap-2">
                                <template v-if="category.questions[currentQuestionIndex].is_multiple_choice">
                                    <input type="checkbox"
                                        :checked="answers[category.questions[currentQuestionIndex].id]?.includes(ans.id)"
                                        @change="() => toggleAnswer(category.questions[currentQuestionIndex].id, ans.id, true)"
                                        class="h-4 w-4 cursor-pointer accent-primary" />
                                </template>
                                <template v-else>
                                    <input type="radio"
                                        :name="'question-' + category.questions[currentQuestionIndex].id"
                                        :checked="answers[category.questions[currentQuestionIndex].id]?.includes(ans.id)"
                                        @change="toggleAnswer(category.questions[currentQuestionIndex].id, ans.id, false)"
                                        class="h-4 w-4 text-primary focus:ring-primary border-gray-300" />
                                </template>
                                <label>{{ ans.answer }}</label>
                            </div>
                        </div>

                        <div class="flex justify-between mt-6">
                            <Button variant="outline" :disabled="currentQuestionIndex === 0" @click="prevQuestion">
                                Previous
                            </Button>
                            <div class="space-x-2">
                                <Button v-if="currentQuestionIndex < category.questions.length - 1"
                                    @click="nextQuestion">
                                    Next
                                </Button>
                                <Button v-else :disabled="submitting" @click="submitQuiz">
                                    <Spinner v-if="submitting" /> {{ submitting ? 'Submitting...' : 'Submit' }}
                                </Button>
                            </div>
                        </div>
                    </div>
                    <p v-else class="text-gray-500 text-center italic">No questions found for this category.</p>
                </Card>
            </div>

            <div v-else>
                <Card class="p-6 text-center">
                    <h2 class="text-xl font-semibold mb-2">Your Results</h2>
                    <p class="text-lg">
                        You scored <span class="font-bold">{{ result.score }}</span>
                        out of <span class="font-bold">{{ result.total_questions }}</span>
                    </p>
                    <Button class="mt-4" @click="router.visit('/dashboard')">
                        Back to Categories
                    </Button>
                </Card>
            </div>
        </template>
    </div>
</template>
