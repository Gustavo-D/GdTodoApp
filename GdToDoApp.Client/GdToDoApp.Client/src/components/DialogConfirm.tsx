
import type { TransitionProps } from '@mui/material/transitions';
import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogContentText,
    DialogActions,
    Slide,
    Button
} from '@mui/material';
import React from 'react';

const Transition = React.forwardRef(function Transition(
  props: TransitionProps & {
    children: React.ReactElement<any, any>;
  },
  ref: React.Ref<unknown>,
) {
  return <Slide direction="up" ref={ref} {...props} />;
});

type DialogConfirmProps = {
    open: boolean,
    onCancel: () => void,
    onConfirm: () => Promise<void>,
};

export const DialogConfirm: React.FC<DialogConfirmProps> = ({ open, onCancel, onConfirm }) => {
    return (
        <Dialog
            open={open}
            slots={{
            transition: Transition,
            }}
            keepMounted
            onClose={onCancel}
            aria-describedby="alert-dialog-slide-description"
        >
            <DialogTitle>{"Confirmar exclusão?"}</DialogTitle>
            <DialogContent>
            <DialogContentText id="alert-dialog-slide-description">
                Ao confirmar a exclusão da tarefa, você não conseguirá voltar atrás.
            </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={onCancel}>Cancelar</Button>
                <Button onClick={onConfirm}>Confirmar</Button>
            </DialogActions>
        </Dialog>
    )
}