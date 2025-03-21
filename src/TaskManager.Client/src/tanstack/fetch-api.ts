import { config } from "../config";
import {
  AtualizarTarefaForm,
  CriarTarefaForm,
  ErroResponse,
  Tarefa,
} from "../config/types";

export const ObterTarefas = async (): Promise<Tarefa[]> => {
  const response = await fetch(`${config.backend_domain}/api/v1/tarefa`);
  return await response.json();
};

export const CriarTarefa = async (
  data: CriarTarefaForm
): Promise<Tarefa | ErroResponse> => {
  const response = await fetch(`${config.backend_domain}/api/v1/tarefa`, {
    method: "POST",
    body: JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  });
  return await response.json();
};

interface AtualizarTarefa {
  id: number;
  data: AtualizarTarefaForm;
}
export const AtualizarTarefa = async ({
  id,
  data,
}: AtualizarTarefa): Promise<Tarefa | ErroResponse> => {
  const response = await fetch(`${config.backend_domain}/api/v1/tarefa/${id}`, {
    method: "PUT",
    body: JSON.stringify({ ...data }),
    headers: {
      "Content-Type": "application/json",
    },
  });
  return await response.json();
};

export const ExcluirTarefa = async (
  id: number
): Promise<ErroResponse | null> => {
  const response = await fetch(`${config.backend_domain}/api/v1/tarefa/${id}`, {
    method: "DELETE",
  });

  if (response.status !== 204) return await response.json();
  return null;
};
