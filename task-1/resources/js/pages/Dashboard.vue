<script setup lang="ts">
import AppLayout from '@/layouts/AppLayout.vue';
import { dashboard } from '@/routes';
import { Category, type BreadcrumbItem } from '@/types';
import { Head, usePage } from '@inertiajs/vue3';
import { onMounted, ref } from 'vue';
import QuizCard from '@/components/Cards/QuizCard.vue';
import QuizCardSkeleton from '@/components/Skeletons/QuizCardSkeleton.vue';
import CategoryModal from '@/components/Modals/CategoryModal.vue';

const breadcrumbs: BreadcrumbItem[] = [
    {
        title: 'Quizes',
        href: dashboard().url,
    },
];

const page = usePage();
const categoryModalRef = ref<InstanceType<typeof CategoryModal> | null>(null);
const loading = ref<boolean>(false);
const categories = ref<Category[]>([]);
const choosedCategory = ref<Category | null>(null);

const fetchCategories = async () => {
    loading.value = true;
    try {
        const response = await fetch('/api/categories', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('token'),
            }
        });

        const data = await response.json();
        if (!data.success) {
            throw new Error(data.message);
        }

        categories.value = data.categories;
    } catch (error) {
        console.error(error);
        throw error;
    } finally {
        loading.value = false;
    }
}

const handleEditQuiz = (category: Category) => {
    choosedCategory.value = category;
    if (categoryModalRef.value) {
        categoryModalRef.value.open = true;
    }
}

onMounted(async () => {
    await fetchCategories();
});
</script>

<template>

    <Head title="Quizes" />

    <AppLayout :breadcrumbs="breadcrumbs">
        <div class="flex h-full flex-1 flex-col gap-4 overflow-x-auto rounded-xl p-4">
            <div v-if="page.props.auth.user.is_admin" class="flex w-full items-center justify-end">
                <CategoryModal ref="categoryModalRef" :category="choosedCategory" @created="fetchCategories"
                    @closed="choosedCategory = null" />
            </div>
            <div class="grid auto-rows-min gap-4 w-full md:grid-cols-3">
                <QuizCardSkeleton v-if="loading" v-for="i in 9" />
                <QuizCard v-else v-for="category in categories" :key="category.id" :category="category"
                    @deleted="fetchCategories" @edit="handleEditQuiz" />
            </div>
        </div>
    </AppLayout>
</template>
