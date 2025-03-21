import { FC, useState } from "react";
import { Tarefa } from "../../config/types";
import { Modal } from "../ui/Modal";
import { AtualizarTarefa } from "./AtualizarTarefa";
import { useExcluirTarefa } from "../../tanstack/mutations";
import { toast } from "react-toastify";
import { queryClient } from "../../main";

interface CardProps {
  tarefa: Tarefa;
}

export const Card: FC<CardProps> = ({ tarefa }) => {
  const [open, setOpen] = useState(false);
  const { mutateAsync, isPending, isError } = useExcluirTarefa();

  const handleShowModal = () => {
    setOpen((prevState) => !prevState);
  };

  const handleExcluirTarefa = async () => {
    const confirm = window.confirm("Tem certeza de que deseja excluir?");
    if (!confirm) return;

    const resp = await mutateAsync(tarefa.id);
    if (resp) {
      toast.error(resp.erros.join(", "));
    }
    queryClient.invalidateQueries({ queryKey: ["tarefas"] });
  };

  const statusColors = {
    0: "bg-yellow-100 border-yellow-500 text-yellow-800",
    1: "bg-blue-100 border-blue-500 text-blue-800",
    2: "bg-green-100 border-green-500 text-green-800",
  };

  if (isError) toast.error("Oops! Ocorreu um erro.");

  return (
    <div
      className={`border p-4 rounded-lg shadow-md ${
        statusColors[tarefa.status]
      } m-2`}
    >
      <h3 className="text-lg font-semibold text-gray-800 text-center">
        {tarefa.titulo}
      </h3>

      {tarefa.descricao && (
        <p className="text-sm text-gray-700 mt-1">{tarefa.descricao}</p>
      )}

      <span
        className={`inline-block text-xs font-bold px-2 py-1 mt-2 rounded ${
          statusColors[tarefa.status]
        }`}
      >
        {tarefa.status === 0
          ? "Pendente"
          : tarefa.status === 1
          ? "Em Progresso"
          : "Concluído"}
      </span>

      {tarefa.criadaEm && (
        <p className="text-xs text-gray-500 mt-1">
          Criado em: {new Date(tarefa.criadaEm).toLocaleDateString()}
        </p>
      )}

      {tarefa.concluidaEm && (
        <p className="text-xs text-gray-500 mt-1">
          Concluído em: {new Date(tarefa.concluidaEm).toLocaleDateString()}
        </p>
      )}

      <div className="flex justify-between items-center mt-3">
        <button
          onClick={handleShowModal}
          className="bg-gray-200 hover:bg-gray-300 text-gray-800 px-3 py-1 rounded-md cursor-pointer"
        >
          {tarefa.status === 2 ? "👀 Ver" : "✏️ Editar"}
        </button>
        <button
          onClick={handleExcluirTarefa}
          disabled={isPending}
          className="bg-red-500 hover:bg-red-600 text-white px-3 py-1 rounded-md cursor-pointer"
        >
          🗑️ Deletar
        </button>
      </div>

      {open && (
        <Modal open={open} onClose={handleShowModal}>
          <AtualizarTarefa tarefa={tarefa} casoSucesso={() => setOpen(false)} />
        </Modal>
      )}
    </div>
  );
};
