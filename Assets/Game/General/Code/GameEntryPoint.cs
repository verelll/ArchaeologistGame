using Archaeologist.MainModule;
using UnityEngine;

namespace Archaeologist.General
{
	public sealed class GameEntryPoint : MonoBehaviour
	{
		[Header("Main")]
		[SerializeField] private GridView _gridView;
		[SerializeField] private MainInfoView _infoView;
		
		[Header("Rewards")]
		[SerializeField] private UIRewardsContainer _uiRewardsContainer;
		[SerializeField] private UIRewardBagView _rewardsBagView;

		[Header("Restart")] 
		[SerializeField] private UIRestartView _restartView;
		
		private MainStorage _mainStorage;
		private GridInteractableService _interactableService;
		private RewardsViewController _rewardsViewController;
		private RewardsService _rewardsService;
		private MainInfoModel _mainInfoModel;
		private MainService _mainService;
		private RestartService _restartService;
		private FinishGameService _finishGameService;

		private void Start()
		{
			_mainStorage = new MainStorage();
			_mainStorage.Load();
			
			_interactableService = new GridInteractableService();
			_mainInfoModel = new MainInfoModel(_mainStorage.SaveData);
			_rewardsViewController = new RewardsViewController(_gridView, _uiRewardsContainer, _rewardsBagView);
			_rewardsService = new RewardsService(_rewardsViewController, _mainInfoModel, _mainStorage.SaveData);
			_mainService = new MainService(_mainStorage.SaveData, _interactableService, _rewardsService, _mainInfoModel, _gridView, _infoView);
			_restartService = new RestartService(_mainInfoModel, _mainService, _rewardsService, _restartView);
			_finishGameService = new FinishGameService(_mainInfoModel, _rewardsService, _restartService);

			Init();
		}

		private void OnDestroy()
		{
			_mainStorage.Save();
			Dispose();
		}
		
		private void Update()
		{
			_interactableService?.Update();
		}

		private void Init()
		{
			_restartService?.Init();
			_mainService?.Init();
			_rewardsService?.Init();
			_finishGameService?.Init();
		}

		private void Dispose()
		{
			_restartService?.Dispose();
			_mainService?.Dispose();
			_rewardsService?.Dispose();
			_finishGameService?.Dispose();
		}
	}
}
