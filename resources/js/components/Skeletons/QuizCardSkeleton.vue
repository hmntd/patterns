<script setup lang="ts">
import { Card } from '@/components/ui/card'
</script>

<template>
    <Card class="p-4 flex flex-col justify-between animate-pulse cursor-default select-none">
        <div class="flex items-start justify-between">
            <div class="space-y-2">
                <div class="h-5 w-40 bg-muted rounded-md"></div>

                <div class="h-4 w-28 bg-muted rounded-md"></div>
            </div>

            <div class="h-6 w-14 bg-muted rounded-full"></div>
        </div>

        <div class="mt-3 space-y-2">
            <div class="h-4 w-full bg-muted rounded-md"></div>
            <div class="h-4 w-5/6 bg-muted rounded-md"></div>
            <div class="h-4 w-3/4 bg-muted rounded-md"></div>
        </div>
    </Card>
</template>
