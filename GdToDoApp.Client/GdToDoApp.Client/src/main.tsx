import React, { type JSX } from "react";
import { createBrowserRouter, Navigate, RouterProvider } from "react-router";
import './index.css'
import LoginPage from './pages/LoginPage.tsx'
import DashboardPage, { getCategories, getTasks } from './pages/DashboardPage.tsx'
import ReactDOM from "react-dom/client";
import { httpGet } from "./services/apiService.tsx";

const ProtectedRoute = ({ children }: { children: JSX.Element }) => {
  const token = localStorage.getItem('token');
  if(!token){
    return <Navigate to="/" replace />;
  }
  

  return children;
};

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

const ErrorBoundary = () => {
  return (
    <div style={{ padding: 32, textAlign: 'center' }}>
      <h2>Ocorreu um erro inesperado.</h2>
      <p>Tente recarregar a página ou voltar para a tela inicial.</p>
      <a href="/">Ir para Login</a>
    </div>
  );
}

const router = createBrowserRouter([
  {
    index: true,
    path: "/",
    element: <LoginPage />,
    errorElement: <ErrorBoundary />
  },
  {
    path: "/dashboard",
    element: (<ProtectedRoute><DashboardPage /></ProtectedRoute>),
    loader: async () => {
      const usersPromise = httpGet('usuario');
      const [ tasksData, categoriesData, usersResponse ] = await Promise.all([getTasks(), getCategories(), usersPromise])
      return { tasksData, categoriesData, usersData: usersResponse.data.map((x: any) => { return { id: x.id, label: x.username }}) }
    },
    errorElement: <Navigate to="/" replace />
  }
]);

root.render(
  <React.StrictMode>
    <RouterProvider router={router} />  
  </React.StrictMode>
);