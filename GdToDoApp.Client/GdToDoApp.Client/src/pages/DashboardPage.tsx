
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
import AddIcon from '@mui/icons-material/Add';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import SearchIcon from '@mui/icons-material/Search';
import ReplayIcon from '@mui/icons-material/Replay';
import type { Task } from '../types/task';
import { useLoaderData } from 'react-router';
import { httpDelete, httpGet, httpPatch, httpPost } from '../services/apiService';
import { jwtDecode } from "jwt-decode";
import { DeleteOutline } from '@mui/icons-material';
import { Toast } from '../components/Toast';
import { DialogConfirm } from '../components/DialogConfirm';

export const getTasks = async () => {
  const response = await httpGet('tarefa');
  return response.data;
}

export const getCategories = async () => {
  const response = await httpGet('tarefa/categories');
  return response.data;
}

type FilterUser = { id: number, label: string };

const DashboardPage: React.FC = () => {
    const { tasksData, categoriesData, usersData } = useLoaderData();
    const [tasks, setTasks] = useState<Task[]>(tasksData || []);
    const [adding, setAdding] = useState(false);
    const [form, setForm] = useState<Task>({ id: 0, title: '', description: '', category: '' });
    const [editingId, setEditingId] = useState<number | null>(null);
    const [editForm, setEditForm] = useState<Task>({ id: 0, title: '', description: '', category: '' });
    const [categories, setCategories] = useState<string[]>(categoriesData || []);
    const [toast, setToast] = useState({ open: false, message: '', severity: 'error' });
    const [taskDeleting, setTaskDeleting] = useState<number>(0);
    const [filter, setFilter] = useState<{users: FilterUser[], categories: string[]}>({users: [], categories: []});

    const onError = async (error: string) => {
        setToast({ open: true, message: error, severity: 'error' });
    };
    
    const handleAddClick = () => {
        setAdding(true);        
        setEditingId(null);
    };

    const handleFormChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleCategoryChange = (_: any, value: string | null) => {
        setForm({ ...form, category: value || '' });
    };

    const handleFormSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        formSubmit(form)
    };

    const handleEditCategoryChange = (_: any, value: string | null) => {
        setEditForm({ ...editForm, category: value || '' });
    };

    const handleEditClick = (task: Task) => {
        setEditingId(task.id);
        setAdding(false);
        setEditForm({
            id: task.id,
            title: task.title,
            description: task.description,
            category: task.category,
            isCompleted: task.isCompleted,
            userId: task.userId            
        });
    };

    const handleEditFormChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setEditForm({ ...editForm, [e.target.name]: e.target.value });
    };

    const formSubmit = async (formData: Task) => {
        const successMessage = adding ? 'Tarefa criada com sucesso!' : 'Tarefa editada com sucesso!'
        const onSuccess = async () => {
            setToast({ open: true, message: successMessage, severity: 'success' });
            setTasks(await getTasks());
            setCategories(await getCategories())
            setEditingId(null);
            setAdding(false);
            setForm({ id: 0, title: '', description: '', category: '' });
        }

        if(adding){
            const token = localStorage.getItem('token');
            const decoded: any = jwtDecode(token!);
            formData.userId = decoded.certserialnumber;
            await httpPost('tarefa', formData, onSuccess, onError);
        }
        else
            await httpPatch('tarefa', formData, onSuccess, onError);
    }

    const handleEditFormSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        formSubmit(editForm);
    };

    const handleChangeIsCompletedTask = async (data: Task) => {
        const onSuccess = async () => {
            const partialMessage = data.isCompleted === 0 ? 'reaberta' : 'completada' ;
            setToast({ open: true, message: `Tarefa ${partialMessage} com sucesso.`, severity: 'success' });
            setTasks(await getTasks()); 
        }

        data.isCompleted = data.isCompleted === 0 ? 1 : 0;
        await httpPatch('tarefa', data, onSuccess, onError);
    };

    const handleDeleteClick = (id: number) => {
        setTaskDeleting(id);
    }

    const handleDelete = async () => {
        const onSuccess = async () => {
            setToast({ open: true, message: 'Tarefa excluída com sucesso.', severity: 'success' });
            setTasks(await getTasks());
            setCategories(await getCategories())
            setTaskDeleting(0);
        }

        await httpDelete(`tarefa/${taskDeleting}`, onSuccess, onError);
    }

    const handleFilterClick = async () => {
        const response = await httpGet('tarefa/filter', { userId: filter.users.map(x => x.id), category: filter.categories });
        setTasks(response.data);
    }

    return (
        <Container maxWidth="md" sx={{ py: 4 }}>
            <Typography variant="h4" mb={3} align="center">Dashboard de Tarefas</Typography>
            <fieldset style={{borderRadius: '.75rem', padding: '.5rem', fontFamily: 'sans-serif', color: '#333'}}>
                <legend>Filtros</legend>
                <Grid container spacing={2}>
                    <Grid size={{ xs: 12, md: 5 }}>
                        <Autocomplete
                            multiple
                            size="small"
                            id="tags-standard"
                            options={usersData || []}
                            getOptionLabel={(option: any) => option.label}
                            defaultValue={[]}
                            value={filter.users}
                            onChange={(_, value) => setFilter({...filter, users: value})}
                            renderInput={(params) => (
                                <TextField
                                    {...params}
                                    label="Usuários"
                                    placeholder="Usuários"
                                />
                            )}
                            fullWidth
                        />
                    </Grid>
                    <Grid size={{ xs: 12, md: 5 }}>
                        <Autocomplete
                            multiple
                            size="small"
                            id="tags-standard"
                            options={categories || []}
                            defaultValue={[]}
                            value={filter.categories}
                            onChange={(_, value) => setFilter({...filter, categories: value})}
                            renderInput={(params) => (
                                <TextField
                                    {...params}
                                    label="Categorias"
                                    placeholder="Categorias"
                                />
                            )}
                            fullWidth
                        />
                    </Grid>
                    <Grid size={{ xs: 12, md: 2 }}>
                        <Button variant="contained" color="inherit" onClick={handleFilterClick} startIcon={<SearchIcon />}>
                            Filtrar
                        </Button>   
                    </Grid>
                </Grid>
            </fieldset>
            <Box display="flex" mb={2} mt={2}>
                <Button variant="contained" color="primary" onClick={handleAddClick} startIcon={<AddIcon />}>
                    Adicionar Tarefa
                </Button>        
            </Box>                        
            <Grid container spacing={2}>
                {tasks.map(task => (
                    <Grid size={{ xs: 12, md: 4 }} key={task.id}>
                        <Card sx={task.isCompleted ? { opacity: 0.6, backgroundColor: '#e0e0e0' } : {}}>
                            <CardContent sx={{ position: 'relative' }}>
                                <Box display="flex" justifyContent="space-between" alignItems="flex-start">
                                    <Typography variant="h6" sx={{ wordBreak: 'break-word', textDecoration: task.isCompleted ? 'line-through' : 'none' }}>
                                        {editingId === task.id ? (
                                            <TextField
                                                label="Titulo"
                                                name="title"
                                                value={editForm.title}
                                                onChange={handleEditFormChange}
                                                size="small"
                                                fullWidth
                                                required
                                            />
                                        ) : (
                                            task.title
                                        )}
                                    </Typography>
                                    {!task.isCompleted ? (
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
                                {!editingId && (
                                    <Box display="flex" justifyContent="space-between" alignItems="flex-start">
                                        <Typography variant="caption" sx={{ wordBreak: 'break-word', textDecoration: task.isCompleted ? 'line-through' : 'none' }}>
                                            <b>Usuário:</b> {task.user?.username}
                                        </Typography>
                                    </Box>
                                )}
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
                                            required
                                        />
                                        <Button type="submit" variant="contained" color="primary" size="small" sx={{ mt: 1 }}>
                                            Salvar
                                        </Button>
                                    </Box>
                                ) : (
                                    <>
                                        <Typography variant="body2" color="text.secondary" mb={1}>{task.category}</Typography>
                                        <Typography variant="body1" sx={{ textDecoration: task.isCompleted ? 'line-through' : 'none' }}>{task.description}</Typography>
                                        {!task.isCompleted ? (
                                            <Button
                                                variant="outlined"
                                                color="success"
                                                size="small"
                                                startIcon={<CheckCircleIcon />}
                                                sx={{ mt: 2 }}
                                                fullWidth
                                                onClick={() => handleChangeIsCompletedTask(task)}
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
                                                onClick={() => handleChangeIsCompletedTask(task)}
                                            >
                                                REABRIR
                                            </Button>
                                        )}                                        
                                        <Button
                                            variant="outlined"
                                            color="error"
                                            size="small"
                                            startIcon={<DeleteOutline />}
                                            fullWidth
                                            onClick={() => handleDeleteClick(task.id)}
                                            style={{marginTop: '.55rem'}}
                                        >
                                            EXCLUIR
                                        </Button>
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
            <Toast config={toast} onClose={() => { setToast({ ...toast, open: false }) }} />
            <DialogConfirm open={taskDeleting > 0} onConfirm={handleDelete} onCancel={() => setTaskDeleting(0) } />
        </Container>  
    );
};

export default DashboardPage;