import { Snackbar, Alert } from '@mui/material';

type ToastType = { config: {open: boolean, severity: any, message: string }, onClose: () => void };

export const Toast: React.FC<ToastType> = ({ config, onClose }) => {
    return (
        <Snackbar
            open={config.open}
            autoHideDuration={4000}
            onClose={onClose}
            anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
        >
            <Alert severity={config.severity} sx={{ width: '100%' }}>
                {config.message}
            </Alert>
        </Snackbar>
    )
}