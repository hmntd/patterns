<script setup lang="ts">
import { ref, watch } from 'vue';
import DialogClose from '../ui/dialog/DialogClose.vue';
import DialogContent from '../ui/dialog/DialogContent.vue';
import DialogDescription from '../ui/dialog/DialogDescription.vue';
import DialogFooter from '../ui/dialog/DialogFooter.vue';
import DialogHeader from '../ui/dialog/DialogHeader.vue';
import DialogTitle from '../ui/dialog/DialogTitle.vue';
import Spinner from '../ui/spinner/Spinner.vue';
import { Category } from '@/types';
import Button from '../ui/button/Button.vue';
import Label from '../ui/label/Label.vue';
import { useForm, usePage } from '@inertiajs/vue3';
import Input from '../ui/input/Input.vue';
import { Trash } from 'lucide-vue-next';
import Checkbox from '../ui/checkbox/Checkbox.vue';
import Textarea from '../ui/textarea/Textarea.vue';

interface Props {
    open: boolean;
    category: Category | null;
}

const props = defineProps<Props>();
const emits = defineEmits(['created']);

const page = usePage();
const loading = ref(false);

const form = useForm({
    name: props.category?.name ?? '',
    description: props.category?.description ?? '',
});

const questions = ref<any[]>(props.category?.questions ?? []);

const addQuestion = () => {
    questions.value.push({
        text: '',
        multi_correct: false,
        answers: [{ text: '', is_correct: false }],
    });
};

const removeQuestion = (index: number) => {
    questions.value.splice(index, 1);
};

const addAnswer = (qIndex: number) => {
    questions.value[qIndex].answers.push({ text: '', is_correct: false });
};

const removeAnswer = (qIndex: number, aIndex: number) => {
    questions.value[qIndex].answers.splice(aIndex, 1);
};

const toggleCorrectAnswer = (question: any, answerIndex: number, value: string | boolean) => {
    if (question.multi_correct) {
        question.answers[answerIndex].is_correct = value;
    } else {
        question.answers.forEach((a: any, i: number) => {
            a.is_correct = i === answerIndex ? value : false;
        });
    }
};

watch(
    () => props.category,
    (newCategory) => {
        if (newCategory) {
            form.name = newCategory.name;
            form.description = newCategory.description;
            questions.value = newCategory.questions.map((q: any) => ({
                text: q.question,
                multi_correct: q.is_multiple_choice,
                id: q.id,
                answers: q.answers.map((a: any) => ({
                    text: a.answer,
                    is_correct: a.is_correct,
                    id: a.id,
                })),
            }));
        } else {
            form.name = '';
            form.description = '';
            questions.value = [];
        }
    },
    { immediate: true }
);

const apiFetch = async (url: string, data?: any, method: string = 'POST') => {
    const response = await fetch(url, {
        method,
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
        },
        body: data ? JSON.stringify(data) : undefined,
    });

    if (!response.ok) {
        const text = await response.text();
        throw new Error(`Request failed: ${response.status} ${text}`);
    }

    return response.json();
};

const submit = async () => {
    const errors: string[] = [];

    if (!form.name.trim()) errors.push('Category name is required.');
    if (!form.description.trim()) errors.push('Category description is required.');
    if (questions.value.length === 0) errors.push('You must add at least one question.');

    questions.value.forEach((q, qi) => {
        if (!q.text.trim()) errors.push(`Question #${qi + 1}: text is required.`);
        if (q.answers.length === 0) errors.push(`Question #${qi + 1}: must have at least one answer.`);
        const hasCorrect = q.answers.some((a: any) => a.is_correct);
        if (!hasCorrect) errors.push(`Question #${qi + 1}: at least one correct answer is required.`);
    });

    if (errors.length > 0) {
        alert(errors.join('\n'));
        return;
    }

    loading.value = true;

    try {
        let categoryId: number;

        if (props.category) {
            const updated = await apiFetch(`/api/categories/${props.category.id}`, {
                name: form.name,
                description: form.description,
            }, 'PUT');

            categoryId = updated.category.id;
        } else {
            const created = await apiFetch('/api/categories', {
                name: form.name,
                description: form.description,
            }, 'POST');

            categoryId = created.category.id;
        }

        if (props.category) {
            await apiFetch(`/api/categories/${categoryId}/questions/clear`, {}, 'DELETE');
        }

        for (const q of questions.value) {
            const questionData = await apiFetch('/api/questions', {
                category_id: categoryId,
                question: q.text,
                is_multiple_choice: q.multi_correct,
                created_by: page.props.auth.user.id,
            });

            const questionId = questionData.question.id;

            for (const a of q.answers) {
                await apiFetch('/api/answers', {
                    question_id: questionId,
                    answer: a.text,
                    is_correct: a.is_correct,
                    created_by: page.props.auth.user.id,
                });
            }
        }

        alert(`✅ Category ${props.category ? 'updated' : 'created'} successfully!`);
        emits('created');
    } catch (err: any) {
        console.error(err);
        alert(`❌ Error while saving data: ${err.message}`);
    } finally {
        loading.value = false;
    }
};
</script>

<template>
    <DialogContent>
        <DialogHeader>
            <DialogTitle>{{ props.category ? 'Edit' : 'Create' }} Category</DialogTitle>
            <DialogDescription>
                Fill in the details for your category below, including name, description, and any questions.
            </DialogDescription>
        </DialogHeader>

        <div class="grid gap-4 py-4">
            <!-- Category name -->
            <div class="grid grid-cols-[1fr_3fr] items-center">
                <Label for="name" class="text-right">Name</Label>
                <Input id="name" v-model="form.name" type="text" placeholder="Enter name of the Category" />
                <p v-if="form.errors.name" class="text-destructive text-sm mt-1">{{ form.errors.name }}</p>
            </div>

            <!-- Category description -->
            <div class="grid grid-cols-[1fr_3fr] items-center">
                <Label for="description" class="text-right">Description</Label>
                <Input id="description" v-model="form.description" type="text"
                    placeholder="Enter description of the Category" />
                <p v-if="form.errors.description" class="text-destructive text-sm mt-1">
                    {{ form.errors.description }}
                </p>
            </div>

            <!-- Questions section -->
            <div class="border-t pt-4 overflow-y-auto max-h-[50vh]">
                <div class="flex justify-between items-center mb-2">
                    <h3 class="font-semibold text-lg">Questions</h3>
                    <Button variant="secondary" size="sm" @click="addQuestion">+ Add Question</Button>
                </div>

                <div v-if="questions.length > 0" class="space-y-6">
                    <div v-for="(q, qi) in questions" :key="qi"
                        class="border border-gray-300 rounded-xl p-4 bg-muted relative">
                        <button class="absolute top-2 right-2 text-gray-500 hover:text-destructive transition-colors"
                            @click="removeQuestion(qi)">
                            <Trash class="w-4 h-4" />
                        </button>

                        <div class="flex flex-col gap-2 mb-3">
                            <Label :for="'question-' + qi">Question {{ qi + 1 }}</Label>
                            <Textarea :id="'question-' + qi" v-model="q.text" placeholder="Enter question text" />
                        </div>

                        <div class="flex items-center gap-2 mb-3 cursor-pointer">
                            <Checkbox v-model="q.multi_correct" :id="'multi-' + qi" />
                            <Label :for="'multi-' + qi">Allow multiple correct answers</Label>
                        </div>

                        <div class="space-y-2">
                            <div class="flex justify-between items-center">
                                <Label>Answers</Label>
                                <Button size="sm" variant="secondary" @click="addAnswer(qi)">+ Add Answer</Button>
                            </div>

                            <div v-for="(a, ai) in q.answers" :key="ai" class="flex items-center gap-2">
                                <Input v-model="a.text" placeholder="Answer text" class="flex-1" />

                                <Checkbox class="cursor-pointer" :model-value="a.is_correct"
                                    @update:model-value="toggleCorrectAnswer(q, ai, $event)" />

                                <Button variant="ghost" size="icon" @click="removeAnswer(qi, ai)">
                                    <Trash class="w-4 h-4" />
                                </Button>
                            </div>
                        </div>
                    </div>
                </div>

                <p v-else class="text-gray-500 text-sm italic mt-2">No questions yet.</p>
            </div>
        </div>

        <!-- Footer -->
        <DialogFooter>
            <DialogClose as-child>
                <Button variant="outline" :disabled="loading" class="cursor-pointer">Close</Button>
            </DialogClose>

            <Button type="submit" :disabled="loading" class="cursor-pointer" @click="submit">
                <Spinner v-if="loading" />{{ loading ? "Saving..." : "Save" }}
            </Button>
        </DialogFooter>
    </DialogContent>
</template>
