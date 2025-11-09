<script setup lang="ts">
import { Trophy, Edit3, Trash2 } from 'lucide-vue-next'
import Card from '../ui/card/Card.vue'
import Button from '../ui/button/Button.vue'
import { router, usePage } from '@inertiajs/vue3'
import { Category } from '@/types'
import Badge from '../ui/badge/Badge.vue'

const props = defineProps<{
    category: Category
}>();

const emits = defineEmits(['deleted', 'edit']);

const page = usePage();
const isAdmin = page.props.auth.user.is_admin;

const handleClick = () => {
    router.visit('/categories/' + props.category.id);
}

const handleLeaderboard = (event: Event) => {
    event.stopPropagation();
    router.visit(`/categories/${props.category.id}/leaderboard`);
}

const handleEdit = (event: Event) => {
    event.stopPropagation();
    emits('edit', props.category);
}

const handleDelete = async (event: Event) => {
    event.stopPropagation()
    if (!confirm(`Are you sure you want to delete "${props.category.name}"?`)) return

    try {
        await fetch(`/api/categories/${props.category.id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token'),
            },
        })
        alert('✅ Category deleted successfully.')
        emits('deleted');
    } catch (err) {
        console.error(err)
        alert('❌ Error deleting category.')
    }
}
</script>

<template>
    <Card
        class="p-4 flex flex-col justify-between cursor-pointer hover:bg-sidebar-accent hover:text-sidebar-accent-foreground transition-shadow"
        @click="handleClick">
        <div class="flex items-start justify-between">
            <div>
                <h3 class="text-lg font-semibold text-foreground">
                    {{ props.category.name }}
                </h3>
                <p v-if="props.category.description" class="mt-1 text-sm text-muted-foreground line-clamp-2">
                    {{ props.category.description }}
                </p>
            </div>

            <Badge variant="secondary" v-if="props.category.has_passed">Passed: {{ props.category.result.score }} / {{ props.category.result.total_questions }}</Badge>
        </div>

        <div class="mt-4 flex flex-wrap items-center gap-2">
            <Button size="sm" variant="secondary" class="flex items-center gap-1 cursor-pointer" @click="handleLeaderboard">
                <Trophy class="w-4 h-4" />
            </Button>

            <template v-if="isAdmin">
                <Button size="sm" variant="outline" class="flex items-center gap-1 cursor-pointer" @click="handleEdit">
                    <Edit3 class="w-4 h-4" />
                </Button>

                <Button size="sm" variant="destructive" class="flex items-center gap-1 cursor-pointer" @click="handleDelete">
                    <Trash2 class="w-4 h-4" />
                </Button>
            </template>
        </div>
    </Card>
</template>
