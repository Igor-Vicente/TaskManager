import { useState } from "react";
import { Card } from "./components/tarefa/Card";
import { CriarTarefa } from "./components/tarefa/CriarTarefa";
import { Modal } from "./components/ui/Modal";
import { useObterTarefas } from "./tanstack/queries";
import { Loader } from "./components/ui/Loader";
import { toast } from "react-toastify";

export const App = () => {
  const { data, isPending, isError } = useObterTarefas();
  const [open, setOpen] = useState(false);

  const handleShowModal = () => {
    setOpen((prevState) => !prevState);
  };

  if (isError) toast.error("Oops! Ocorreu um erro.");

  return (
    <div className="p-4 min-h-screen bg-gradient-to-br from-gray-100 to-gray-200">
      <div className="flex justify-center m-4">
        <h1 className="text-4xl bg-gradient-to-r from-yellow-600 via-blue-500 to-green-400 inline-block text-transparent bg-clip-text">
          Gerenciador de Tarefas
        </h1>
      </div>
      <div className="flex justify-center mb-4">
        <button
          onClick={handleShowModal}
          className="bg-amber-500 hover:bg-amber-600 text-white px-4 py-2 rounded-lg shadow-md cursor-pointer"
        >
          + Nova Tarefa
        </button>
      </div>

      <div className="grid md:grid-cols-3 gap-6 mt-4">
        {/* Coluna 1 - Pendentes */}
        <div className="bg-white p-5 rounded-lg shadow-md border-t-4 border-yellow-400">
          <h2 className="text-xl font-semibold text-yellow-600">Pendentes</h2>
          <div className="mt-2">
            {isPending && <Loader />}
            {data
              ?.filter((t) => t.status === 0)
              .map((tarefa) => (
                <Card tarefa={tarefa} key={tarefa.id} />
              ))}
          </div>
        </div>

        {/* Coluna 2 - Em Progresso */}
        <div className="bg-white p-5 rounded-lg shadow-md border-t-4 border-blue-500">
          <h2 className="text-xl font-semibold text-blue-600">Em Progresso</h2>
          <div className="mt-2">
            {data
              ?.filter((t) => t.status === 1)
              .map((tarefa) => (
                <Card tarefa={tarefa} key={tarefa.id} />
              ))}
          </div>
        </div>

        {/* Coluna 3 - Concluídas */}
        <div className="bg-white p-5 rounded-lg shadow-md border-t-4 border-green-500">
          <h2 className="text-xl font-semibold text-green-600">Concluídas</h2>
          <div className="mt-2">
            {data
              ?.filter((t) => t.status === 2)
              .map((tarefa) => (
                <Card tarefa={tarefa} key={tarefa.id} />
              ))}
          </div>
        </div>
      </div>

      {open && (
        <Modal open={open} onClose={handleShowModal}>
          <CriarTarefa casoSucesso={() => setOpen(false)} />
        </Modal>
      )}
    </div>
  );
};
