import { FC, useEffect, useRef } from "react";
import { createPortal } from "react-dom";

interface ModalProps {
  children: React.ReactNode;
  open: boolean;
  onClose: () => void;
}

export const Modal: FC<ModalProps> = ({ open, onClose, children }) => {
  const dialogRef = useRef<HTMLDialogElement | null>(null);

  useEffect(() => {
    const modal = dialogRef.current;
    if (!modal) return;

    if (open) {
      modal.showModal();
    } else {
      modal.close();
    }
  }, [open]);

  const handleCancel = (event: React.SyntheticEvent<HTMLDialogElement>) => {
    event.preventDefault();
    onClose();
  };

  const handleClick = () => {
    onClose();
  };

  return createPortal(
    <dialog
      ref={dialogRef}
      onCancel={handleCancel}
      className="p-10 rounded m-auto md:w-2xl backdrop:bg-black/60"
    >
      <button onClick={handleClick} className="underline float-end">
        Fechar
      </button>
      {children}
    </dialog>,
    document.getElementById("modal")!
  );
};
