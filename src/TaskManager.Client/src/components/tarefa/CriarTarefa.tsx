import { zodResolver } from "@hookform/resolvers/zod";
import { Controller, useForm } from "react-hook-form";
import { criarTarefaSchema, CriarTarefaForm } from "../../config/types";
import { useCriarTarefa } from "../../tanstack/mutations";
import { queryClient } from "../../main";
import { FC } from "react";
import { toast } from "react-toastify";
import { Loader } from "../ui/Loader";

interface CriarTarefaProps {
  casoSucesso: () => void;
}

export const CriarTarefa: FC<CriarTarefaProps> = ({ casoSucesso }) => {
  const {
    control,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CriarTarefaForm>({
    resolver: zodResolver(criarTarefaSchema),
  });

  const { mutateAsync, isPending, isError } = useCriarTarefa();

  const handleCriarTarefa = async (data: CriarTarefaForm) => {
    const resp = await mutateAsync(data);
    // Verifica se a resposta é um erro
    if ("sucesso" in resp && resp.sucesso === false) {
      toast.error(resp.erros.join(", "));
    } else {
      queryClient.invalidateQueries({ queryKey: ["tarefas"] });
      reset();
      casoSucesso();
    }
  };

  if (isError) toast.error("Oops! Ocorreu um erro.");

  return (
    <form
      onSubmit={handleSubmit(handleCriarTarefa)}
      className="bg-white shadow-md rounded-lg p-6 w-full max-w-lg mx-auto"
    >
      <h1 className="text-3xl font-bold my-4 text-center text-amber-600">
        ➕ Adicionar Tarefa
      </h1>

      <div className="flex flex-col gap-2">
        <label className="text-sm font-medium text-zinc-700">📌 Título</label>
        <Controller
          name="titulo"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <>
              <input
                type="text"
                {...field}
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

      <div className="flex flex-col gap-2 mt-4">
        <label className="text-sm font-medium text-zinc-700">
          📝 Descrição
        </label>
        <Controller
          name="descricao"
          control={control}
          render={({ field }) => (
            <>
              <textarea
                {...field}
                rows={4}
                className="w-full rounded-lg resize-y border border-zinc-300 px-4 py-2 text-zinc-800 shadow-sm transition-all duration-300 ease-in-out focus:outline-none focus:ring-2 focus:ring-amber-400 hover:border-amber-400"
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
      {isPending && <Loader />}
      <button
        disabled={isPending}
        className="w-full mt-6 bg-amber-500 text-white font-semibold py-2 rounded-lg shadow-md transition-all duration-300 hover:bg-amber-600 active:scale-95 disabled:hidden"
      >
        💾 Salvar
      </button>
    </form>
  );
};
