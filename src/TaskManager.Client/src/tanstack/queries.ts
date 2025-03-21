import { useQuery } from "@tanstack/react-query";
import { ObterTarefas } from "./fetch-api";

export const useObterTarefas = () => {
  return useQuery({
    queryKey: ["tarefas"],
    queryFn: ObterTarefas,
  });
};
