<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { usePage, router } from '@inertiajs/vue3'
import Button from '@/components/ui/button/Button.vue'
import Card from '@/components/ui/card/Card.vue'
import Spinner from '@/components/ui/spinner/Spinner.vue'

interface Attempt {
    id: number
    user: {
        login: string
    }
    score: number
    created_at: string
}

const props = defineProps<{
    categoryId: number
}>()

const loading = ref(true)
const leaders = ref<Attempt[]>([])
const categoryName = ref<string>('')

const fetchLeaders = async () => {
    loading.value = true
    try {
        const res = await fetch(`/api/categories/${props.categoryId}/top`, {
            headers: {
                Accept: 'application/json',
                Authorization: 'Bearer ' + localStorage.getItem('token'),
            },
        })

        if (!res.ok) throw new Error(`Error: ${res.status}`);
        const data = await res.json();

        leaders.value = data.leaders ?? data;
        console.log(leaders.value);
        categoryName.value = data.category?.name ?? 'Leaderboard';
    } catch (err) {
        console.error(err)
        alert('Failed to load leaderboard.')
    } finally {
        loading.value = false
    }
}

const goBack = () => {
    router.visit('/dashboard');
}

onMounted(async () => {
    await fetchLeaders();
});
</script>

<template>
    <div class="max-w-3xl mx-auto p-6">
        <div class="flex justify-between items-center mb-6">
            <h1 class="text-2xl font-bold text-foreground">
                🏆 {{ categoryName || 'Top 20 Players' }}
            </h1>
            <Button variant="outline" @click="goBack" class="cursor-pointer">Back</Button>
        </div>

        <Card class="p-4">
            <div v-if="loading" class="flex justify-center py-8">
                <Spinner />
            </div>

            <table v-else class="w-full border-collapse text-left text-foreground text-sm">
                <thead class="border-b text-muted-foreground">
                    <tr>
                        <th class="py-2 w-12">#</th>
                        <th class="py-2">Player</th>
                        <th class="py-2">Score</th>
                        <th class="py-2">Date</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(attempt, index) in leaders" :key="attempt.id" class="border-b hover:bg-muted/50">
                        <td class="py-2 font-semibold">{{ index + 1 }}</td>
                        <td class="py-2">{{ attempt.user.login }}</td>
                        <td class="py-2">{{ attempt.score }}</td>
                        <td class="py-2 text-muted-foreground">
                            {{ new Date(attempt.created_at).toLocaleDateString() }}
                        </td>
                    </tr>
                </tbody>
            </table>

            <p v-if="!loading && leaders.length === 0" class="text-center text-muted-foreground py-6 italic">
                No results yet for this category.
            </p>
        </Card>
    </div>
</template>
