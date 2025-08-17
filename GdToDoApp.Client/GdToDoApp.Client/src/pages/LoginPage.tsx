
import React, { useState } from 'react';
import { Container, Box, Typography, TextField, Button, Tabs, Tab, Paper } from '@mui/material';
import { httpPost } from '../services/apiService';

const LoginPage: React.FC = () => {
    const [tab, setTab] = useState(0);
    const [loginData, setLoginData] = useState({ email: '', password: '' });
    const [registerData, setRegisterData] = useState({ name: '', email: '', password: '' });

    const handleTabChange = (_: React.SyntheticEvent, newValue: number) => {
        setTab(newValue);
    };

    const handleLoginChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setLoginData({ ...loginData, [e.target.name]: e.target.value });
    };

    const handleRegisterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setRegisterData({ ...registerData, [e.target.name]: e.target.value });
    };

    const handleLoginSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        httpPost('login', loginData);
    };

    const handleRegisterSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        httpPost('usuario', registerData);
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
                            value={loginData.email}
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
                            value={registerData.email}
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
        </Container>
    );
};

export default LoginPage;