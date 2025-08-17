
import React, { useState } from 'react';
import {
    Container,
    Box,
    Typography,
    Button,
    Card,
    CardContent,
    TextField,
    Grid,
    IconButton,
    Autocomplete
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import CloseIcon from '@mui/icons-material/Close';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import ReplayIcon from '@mui/icons-material/Replay';
import type { Task } from '../types/task';

const DashboardPage: React.FC = () => {
    const [tasks, setTasks] = useState<Task[]>([]);
    const [adding, setAdding] = useState(false);
    const [form, setForm] = useState({ title: '', description: '', category: '' });
    const [editingId, setEditingId] = useState<number | null>(null);
    const [editForm, setEditForm] = useState({ title: '', description: '', category: '' });
    const [categories, setCategories] = useState<string[]>(['Pessoal', 'Trabalho', 'Estudo']);

    const handleAddClick = () => {
        setAdding(true);
    };

    const handleFormChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleCategoryChange = (_: any, value: string | null) => {
        setForm({ ...form, category: value || '' });
    };

    const handleFormSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (form.title && form.description && form.category) {
            if (!categories.includes(form.category)) {
                setCategories([...categories, form.category]);
            }
            setTasks([
                ...tasks,
                {
                    id: Date.now(),
                    title: form.title,
                    description: form.description,
                    category: form.category
                }
            ]);
            setForm({ title: '', description: '', category: '' });
            setAdding(false);
        }
    };

    const handleEditCategoryChange = (_: any, value: string | null) => {
        setEditForm({ ...editForm, category: value || '' });
    };

    const handleEditClick = (task: Task) => {
        setEditingId(task.id);
        setEditForm({
            title: task.title,
            description: task.description,
            category: task.category
        });
    };

    const handleEditFormChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setEditForm({ ...editForm, [e.target.name]: e.target.value });
    };

    const handleEditFormSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (editForm.title && editForm.description && editForm.category && editingId !== null) {
            if (!categories.includes(editForm.category)) {
                setCategories([...categories, editForm.category]);
            }
            setTasks(tasks.map(task =>
                task.id === editingId
                    ? { ...task, ...editForm }
                    : task
            ));
            setEditingId(null);
        }
    };

    const handleCompleteTask = (id: number) => {
        setTasks(tasks.map(task =>
            task.id === id ? { ...task, completed: true } : task
        ));
    };

    const handleReopenTask = (id: number) => {
        setTasks(tasks.map(task =>
            task.id === id ? { ...task, completed: false } : task
        ));
    };

    return (
        <Container maxWidth="md" sx={{ py: 4 }}>
            <Typography variant="h4" mb={3} align="center">Dashboard de Tarefas</Typography>
            <Box display="flex" justifyContent="center" mb={2}>
                <Button variant="contained" color="primary" onClick={handleAddClick}>
                    Adicionar Tarefa
                </Button>
            </Box>
            <Grid container spacing={2}>
                {tasks.map(task => (
                    <Grid size={{ xs: 12, md: 4 }} key={task.id}>
                        <Card sx={task.completed ? { opacity: 0.6, backgroundColor: '#e0e0e0' } : {}}>
                            <CardContent sx={{ position: 'relative' }}>
                                <Box display="flex" justifyContent="space-between" alignItems="flex-start">
                                    <Typography variant="h6" sx={{ wordBreak: 'break-word', textDecoration: task.completed ? 'line-through' : 'none' }}>
                                        {editingId === task.id ? (
                                            <TextField
                                                name="title"
                                                value={editForm.title}
                                                onChange={handleEditFormChange}
                                                size="small"
                                                fullWidth
                                            />
                                        ) : (
                                            task.title
                                        )}
                                    </Typography>
                                    {!task.completed ? (
                                        editingId === task.id ? (
                                            <IconButton size="small" color="error" onClick={() => setEditingId(null)} sx={{ ml: 1 }}>
                                                <CloseIcon />
                                            </IconButton>
                                        ) : (
                                            <IconButton size="small" onClick={() => handleEditClick(task)} sx={{ ml: 1 }}>
                                                <EditIcon />
                                            </IconButton>
                                        )
                                    ) : null}
                                </Box>
                                {editingId === task.id ? (
                                    <Box component="form" onSubmit={handleEditFormSubmit} mt={1}>
                                        <Autocomplete
                                            freeSolo
                                            options={categories}
                                            value={editForm.category}
                                            onChange={handleEditCategoryChange}
                                            onInputChange={(_, value) => setEditForm({ ...editForm, category: value })}
                                            renderInput={(params) => (
                                                <TextField
                                                    {...params}
                                                    label="Categoria"
                                                    name="category"
                                                    size="small"
                                                    margin="dense"
                                                    fullWidth
                                                    required
                                                />
                                            )}
                                        />
                                        <TextField
                                            label="Descrição"
                                            name="description"
                                            value={editForm.description}
                                            onChange={handleEditFormChange}
                                            size="small"
                                            fullWidth
                                            margin="dense"
                                            multiline
                                            maxRows="4"
                                            minRows="2"
                                        />
                                        <Button type="submit" variant="contained" color="primary" size="small" sx={{ mt: 1 }}>
                                            Salvar
                                        </Button>
                                    </Box>
                                ) : (
                                    <>
                                        <Typography variant="body2" color="text.secondary" mb={1}>{task.category}</Typography>
                                        <Typography variant="body1" sx={{ textDecoration: task.completed ? 'line-through' : 'none' }}>{task.description}</Typography>
                                        {!task.completed ? (
                                            <Button
                                                variant="outlined"
                                                color="success"
                                                size="small"
                                                startIcon={<CheckCircleIcon />}
                                                sx={{ mt: 2 }}
                                                fullWidth
                                                onClick={() => handleCompleteTask(task.id)}
                                            >
                                                COMPLETAR
                                            </Button>
                                        ) : (
                                            <Button
                                                variant="outlined"
                                                color="warning"
                                                size="small"
                                                startIcon={<ReplayIcon />}
                                                sx={{ mt: 2 }}
                                                fullWidth
                                                onClick={() => handleReopenTask(task.id)}
                                            >
                                                REABRIR
                                            </Button>
                                        )}
                                    </>
                                )}
                            </CardContent>
                        </Card>
                    </Grid>
                ))}                
                {adding && (
                    <Grid size={{ xs: 12, md: 4 }}>
                        <Card sx={{ bgcolor: '#f5f5f5' }}>
                            <CardContent>
                                <Typography variant="h6" mb={1}>Nova Tarefa</Typography>
                                <Box component="form" onSubmit={handleFormSubmit}>
                                    <TextField
                                        label="Título"
                                        name="title"
                                        value={form.title}
                                        onChange={handleFormChange}
                                        fullWidth
                                        margin="normal"
                                        required
                                    />
                                    <TextField
                                        label="Descrição"
                                        name="description"
                                        value={form.description}
                                        onChange={handleFormChange}
                                        fullWidth
                                        margin="normal"
                                        required
                                        multiline
                                        minRows="2"
                                        maxRows="4"
                                    />
                                    <Autocomplete
                                        freeSolo
                                        options={categories}
                                        value={form.category}
                                        onChange={handleCategoryChange}
                                        onInputChange={(_, value) => setForm({ ...form, category: value })}
                                        renderInput={(params) => (
                                            <TextField
                                                {...params}
                                                label="Categoria"
                                                name="category"
                                                margin="normal"
                                                required
                                                fullWidth
                                            />
                                        )}
                                    />
                                    <Button type="submit" variant="contained" color="success" fullWidth sx={{ mt: 2 }}>
                                        Salvar
                                    </Button>
                                </Box>
                            </CardContent>
                        </Card>
                    </Grid>
                )}
            </Grid>
        </Container>
    );
};

export default DashboardPage;