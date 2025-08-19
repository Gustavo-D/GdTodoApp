export type Task = {
    id: number;
    title: string;
    description: string;
    category: string;
    isCompleted?: 0 | 1;
    userId?: number;
    user?: { username: string }
}