import { FC } from "react";
import {
  AtualizarTarefaForm,
  atualizarTarefaSchema,
  Tarefa,
} from "../../config/types";
import { Controller, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useAtualizarTarefa } from "../../tanstack/mutations";
import { queryClient } from "../../main";
import { toast } from "react-toastify";
import { Loader } from "../ui/Loader";

interface AtualizarTarefaProps {
  tarefa: Tarefa;
  casoSucesso: () => void;
}
export const AtualizarTarefa: FC<AtualizarTarefaProps> = ({
  tarefa,
  casoSucesso,
}) => {
  const {
    handleSubmit,
    control,
    reset,
    formState: { errors },
  } = useForm<AtualizarTarefaForm>({
    resolver: zodResolver(atualizarTarefaSchema),
  });

  const { mutateAsync, isPending, isError } = useAtualizarTarefa();

  const handleAtualizar = async (data: AtualizarTarefaForm) => {
    const resp = await mutateAsync({ id: tarefa.id, data });
    // Verifica se a resposta é um erro
    if ("sucesso" in resp && resp.sucesso === false) {
      toast.error(resp.erros.join(", "));
    } else {
      queryClient.invalidateQueries({ queryKey: ["tarefas"] });
      reset();
      casoSucesso();
    }
  };

  const handleFechar = () => {
    casoSucesso();
  };

  if (isError) toast.error("Oops! Ocorreu um erro.");

  return (
    <form
      onSubmit={handleSubmit(handleAtualizar)}
      className="bg-white shadow-md rounded-lg p-6 w-full max-w-lg mx-auto"
    >
      <h1 className="text-3xl font-bold my-4 text-center text-amber-600">
        ✏️ Atualizar Tarefa
      </h1>
      <div className="flex flex-col gap-2">
        <div className="flex flex-col gap-1">
          <label className="text-sm font-medium text-zinc-700">📌 Título</label>
          <Controller
            name="titulo"
            control={control}
            defaultValue={tarefa.titulo}
            render={({ field }) => (
              <>
                <input
                  type="text"
                  {...field}
                  disabled={tarefa.status === 2}
                  className="w-full rounded-lg border border-zinc-300 px-4 py-2 text-zinc-800 shadow-sm transition-all duration-300 ease-in-out focus:outline-none focus:ring-2 focus:ring-amber-400 hover:border-amber-400"
                />
                {errors.titulo && (
                  <span className="text-xs text-red-500 mt-1">
                    {errors.titulo.message}
                  </span>
                )}
              </>
            )}
          />
        </div>

        <div className="flex flex-col gap-1">
          <label className="text-sm font-medium text-zinc-700">
            📝 Descrição
          </label>
          <Controller
            name="descricao"
            control={control}
            defaultValue={tarefa.descricao || ""}
            render={({ field }) => (
              <>
                <textarea
                  {...field}
                  rows={6}
                  disabled={tarefa.status === 2}
                  className="w-full rounded-lg border border-zinc-300 px-4 py-2 text-zinc-800 shadow-sm transition-all duration-300 ease-in-out focus:outline-none focus:ring-2 focus:ring-amber-400 hover:border-amber-400"
                />
                {errors.descricao && (
                  <span className="text-xs text-red-500 mt-1">
                    {errors.descricao.message}
                  </span>
                )}
              </>
            )}
          />
        </div>

        <div className="flex flex-col gap-1">
          <label className="text-sm font-medium text-zinc-700">
            📅 Data de Conclusão
          </label>
          {tarefa.concluidaEm ? (
            <p className="text-center">
              {new Date(tarefa.concluidaEm).toLocaleDateString()}
            </p>
          ) : (
            <Controller
              name="concluidaEm"
              control={control}
              defaultValue={undefined}
              render={({ field }) => (
                <>
                  <input
                    {...field}
                    type="date"
                    className="w-full rounded-lg border border-zinc-300 px-4 py-2 text-zinc-800 shadow-sm transition-all duration-300 ease-in-out focus:outline-none focus:ring-2 focus:ring-amber-400 hover:border-amber-400"
                  />
                  {errors.descricao && (
                    <span className="text-xs text-red-500 mt-1">
                      {errors.descricao.message}
                    </span>
                  )}
                </>
              )}
            />
          )}
        </div>

        <div className="flex flex-col gap-2">
          <label className="text-sm font-medium text-zinc-700">🟢 Status</label>
          <Controller
            name="status"
            control={control}
            defaultValue={tarefa.status}
            render={({ field }) => (
              <>
                <select
                  {...field}
                  disabled={tarefa.status === 2}
                  className="w-full rounded-lg border border-zinc-300 px-4 py-2 text-zinc-800 shadow-sm transition-all duration-300 ease-in-out focus:outline-none focus:ring-2 focus:ring-amber-400 hover:border-amber-400"
                >
                  <option value={0}>Pendente</option>
                  <option value={1}>Em progresso</option>
                  <option value={2}>Concluída</option>
                </select>
                {errors.status && (
                  <span className="text-xs text-red-500 mt-1">
                    {errors.status.message}
                  </span>
                )}
              </>
            )}
          />
        </div>
        {isPending && <Loader />}
        {tarefa.status === 2 ? (
          <button
            className="bg-zinc-500 hover:bg-zinc-600 text-white px-4 py-2 rounded-lg shadow-md cursor-pointer"
            onClick={handleFechar}
          >
            ✖️ Fechar
          </button>
        ) : (
          <button
            disabled={isPending}
            className="bg-amber-500 hover:bg-amber-600 text-white px-4 py-2 rounded-lg shadow-md cursor-pointer disabled:hidden"
          >
            💾 Salvar
          </button>
        )}
      </div>
    </form>
  );
};
