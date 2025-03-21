import { useMutation } from "@tanstack/react-query";
import { AtualizarTarefa, CriarTarefa, ExcluirTarefa } from "./fetch-api";

export const useCriarTarefa = () => {
  return useMutation({
    mutationFn: CriarTarefa,
  });
};

export const useAtualizarTarefa = () => {
  return useMutation({
    mutationFn: AtualizarTarefa,
  });
};

export const useExcluirTarefa = () => {
  return useMutation({
    mutationFn: ExcluirTarefa,
  });
};
