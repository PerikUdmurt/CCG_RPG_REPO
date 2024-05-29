using CollectionCardGame;
using CollectionCardGame.Gameplay;
using CollectionCardGame.Infrastructure;
using UnityEngine;
using Zenject;

public class MenuStart : MonoBehaviour
{
    [SerializeField] CardConfig _startCard;
    [SerializeField] CardConfig _settingCard;
    [SerializeField] CardConfig _exitCard;
    private CardCreator _cardCreator;
    private CardReciever _cardReciever;


    [Inject]
    private void Construct(CardCreator cardCreator, CardReciever cardReciever)
    {
        _cardCreator = cardCreator;
        _cardReciever = cardReciever;
    }

    private void Awake()
    {
        SpawnCardSlot();
        SpawnCardOnStack();
    }

    private void SpawnCardOnStack()
    {
        _cardCreator.SpawnByConfig(_startCard);
        _cardCreator.SpawnByConfig( _settingCard);
        _cardCreator.SpawnByConfig(_exitCard);
    }

    private void SpawnCardSlot()
    {
        _cardReciever.CreateCardSlot();
    }
}
