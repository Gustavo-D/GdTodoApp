
import React, { useState } from 'react';
import { Container, Box, Typography, TextField, Button, Tabs, Tab, Paper } from '@mui/material';
import { httpPost } from '../services/apiService';
import { useNavigate } from 'react-router';
import type { User } from '../types/user';
import { Toast } from '../components/Toast';

const LoginPage: React.FC = () => {
    const navigate = useNavigate();
    const [tab, setTab] = useState(0);
    const [loginData, setLoginData] = useState<User>({ username: '', password: '' });
    const [registerData, setRegisterData] = useState<User>({ username: '', password: '' });
    const [toast, setToast] = useState({ open: false, message: '', severity: 'error' });

    const handleTabChange = (_: React.SyntheticEvent, newValue: number) => {
        setTab(newValue);
    };

    const handleLoginChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setLoginData({ ...loginData, [e.target.name]: e.target.value });
    };

    const handleRegisterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setRegisterData({ ...registerData, [e.target.name]: e.target.value });
    };

    const submitLogin = async (data: User) => {
        const onSuccess = async (response: {data: {token: string}}) => {
            localStorage.setItem('token', response.data.token);
            navigate('/dashboard');
        };
        const onError = async (error: string) => {
            setToast({ open: true, message: error, severity: 'error' });
        };

        await httpPost('login', data, onSuccess, onError);
    }

    const handleLoginSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await submitLogin(loginData);
    };

    const handleRegisterSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        
        const onSuccess = async () => {
            setToast({ open: true, message: 'Usuário registrado com sucesso!', severity: 'success' });
            setTimeout(() => {
                submitLogin(registerData);
            }, 1500);
        };
        const onError = async (error: string) => {
            setToast({ open: true, message: error, severity: 'error' });
        };

        await httpPost('usuario', registerData, onSuccess, onError);
    };

    return (
        <Container maxWidth="sm" sx={{ minHeight: '100vh', display: 'flex', alignItems: 'center' }}>
            <Paper elevation={3} sx={{ width: '100%', p: 4 }}>
                <Tabs value={tab} onChange={handleTabChange} variant="fullWidth" sx={{ mb: 2 }}>
                    <Tab label="Entrar" />
                    <Tab label="Registrar" />
                </Tabs>
                {tab === 0 ? (
                    <Box component="form" onSubmit={handleLoginSubmit}>
                        <Typography variant="h5" mb={2}>Login</Typography>
                        <TextField
                            label="Username"
                            name="username"
                            type="text"
                            fullWidth
                            margin="normal"
                            value={loginData.username}
                            onChange={handleLoginChange}
                            required
                        />
                        <TextField
                            label="Senha"
                            name="password"
                            type="password"
                            fullWidth
                            margin="normal"
                            value={loginData.password}
                            onChange={handleLoginChange}
                            required
                        />
                        <Button type="submit" variant="contained" color="primary" fullWidth sx={{ mt: 2 }}>
                            Entrar
                        </Button>
                    </Box>
                ) : (
                    <Box component="form" onSubmit={handleRegisterSubmit}>
                        <Typography variant="h5" mb={2}>Registrar Novo Usuário</Typography>
                        <TextField
                            label="Username"
                            name="username"
                            type="text"
                            fullWidth
                            margin="normal"
                            value={registerData.username}
                            onChange={handleRegisterChange}
                            required
                        />
                        <TextField
                            label="Senha"
                            name="password"
                            type="password"
                            fullWidth
                            margin="normal"
                            value={registerData.password}
                            onChange={handleRegisterChange}
                            required
                        />
                        <Button type="submit" variant="contained" color="primary" fullWidth sx={{ mt: 2 }}>
                            Registrar
                        </Button>
                    </Box>
                )}
            </Paper>
            <Toast config={toast} onClose={() => setToast({ ...toast, open: false })} />
        </Container>
    );
};

export default LoginPage;