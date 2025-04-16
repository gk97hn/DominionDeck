public class CardCommand : ICommand
{
    private Card _card;
    private PlayerController _player;
    private bool isExecuted = false;
    public CardCommand(Card card, IController player)
    {
        _card = card;
        _player = (PlayerController)player;
    }

    public bool Execute()
    {
        if (_player.CurrentMana >= _card.Data.manaCost)
        {
            _card.Play();
            _player.SpendMana(_card.Data.manaCost);
            isExecuted = true;
            return isExecuted;
        }
        isExecuted = false;
        return isExecuted;
    }

    public void Undo()
    {
      
    }
}

