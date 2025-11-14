<script setup lang="ts">
import { ref, watch } from 'vue';
import Button from '../ui/button/Button.vue';
import Dialog from '../ui/dialog/Dialog.vue';
import DialogTrigger from '../ui/dialog/DialogTrigger.vue';
import CategoryModalContent from './CategoryModalContent.vue';
import { Category } from '@/types';

const props = defineProps<{
    category: Category | null
}>();

const emits =  defineEmits(['created', 'closed']);
const open = ref<boolean>(false);

const handleCreated = () => {
    emits('created');
    open.value = false;
}

watch(() => open.value, (newVal) => {
    if (!newVal) {
        emits('closed');
    }
});

defineExpose({
    open,
})
</script>

<template>
    <Dialog v-model:open="open">
        <DialogTrigger>
            <Button class="cursor-pointer" variant="outline">
                + Create category
            </Button>
        </DialogTrigger>
        <CategoryModalContent :open="open" :category="props.category" @created="handleCreated" />
    </Dialog>
</template>