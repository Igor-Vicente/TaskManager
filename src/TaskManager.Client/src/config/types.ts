import { z } from "zod";

export interface Tarefa {
  id: number;
  titulo: string;
  descricao: string;
  criadaEm: string;
  concluidaEm: string;
  status: 0 | 1 | 2;
}

export interface ErroResponse {
  sucesso: boolean;
  erros: string[];
}

export interface CriarTarefaForm {
  titulo: string;
  descricao?: string;
}

export const criarTarefaSchema = z.object({
  titulo: z
    .string()
    .min(1, "Forneça um título")
    .max(100, "Título deve ter no máximo 100 caracteres"),
  descricao: z.string().optional(),
});

export interface AtualizarTarefaForm {
  titulo: string;
  descricao?: string;
  concluidaEm?: string;
  status: number;
}

export const atualizarTarefaSchema = z.object({
  titulo: z
    .string()
    .min(1, "Forneça um título")
    .max(100, "Título deve ter no máximo 100 caracteres"),
  descricao: z.string().optional(),
  concluidaEm: z.string().optional(),
  status: z.coerce.number().min(0, "Status inválido").max(2, "Status inválido"),
});
