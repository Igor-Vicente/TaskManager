namespace TaskManager.Domain.Abstractions
{
    public interface INotificador
    {
        void AdicionarNotificacao(string mensagem);
        void AdicionarNotificacao(IEnumerable<string> mensagens);
        bool TemNotificacoes();
        IEnumerable<Notificacao> ObterNotificacoes();
    }

    public class Notificador : INotificador
    {
        private readonly List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new();
        }

        public void AdicionarNotificacao(string mensagem)
        {
            _notificacoes.Add(new Notificacao(mensagem));
        }
        public void AdicionarNotificacao(IEnumerable<string> mensagens)
        {
            List<Notificacao> notificacoes = new();
            foreach (var mensagem in mensagens) notificacoes.Add(new Notificacao(mensagem));
            _notificacoes.AddRange(notificacoes);
        }

        public bool TemNotificacoes() => _notificacoes.Any();

        public IEnumerable<Notificacao> ObterNotificacoes() => _notificacoes;
    }
}
