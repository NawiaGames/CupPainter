#if LK_HAS_LION_ANALYTICS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LionStudios.Suite.Analytics;
#if HAS_LION_GAME_ANALYTICS_SDK
using GameAnalyticsSDK;
#endif
#if HAS_BYTEBREW_SDK
using ByteBrewSDK;
#endif

public class AnalyticsEvents : MonoBehaviour
{
  void Awake()
  {
#if HAS_BYTEBREW_SDK
    ByteBrewInit();
#endif

    EventManager.OnLevelStart += on_level_started;
    EventManager.OnLevelSuccessed += on_level_successed;
    EventManager.OnLevelFail += on_level_failed;
    EventManager.OnLevelRestert += on_level_restarted;
  }
  void OnDestroy()
  {
    EventManager.OnLevelStart += on_level_started;
    EventManager.OnLevelSuccessed += on_level_successed;
    EventManager.OnLevelFail += on_level_failed;
    EventManager.OnLevelRestert += on_level_restarted;
  }

#if HAS_BYTEBREW_SDK
  void ByteBrewInit()
  {
#if UNITY_IOS
    ByteBrew.requestForAppTrackingTransparency((status) =>
    {
      //Case 0: ATTrackingManagerAuthorizationStatusAuthorized
      //Case 1: ATTrackingManagerAuthorizationStatusDenied
      //Case 2: ATTrackingManagerAuthorizationStatusRestricted
      //Case 3: ATTrackingManagerAuthorizationStatusNotDetermined
      Debug.Log("ByteBrew Got a status of: " + status);
      ByteBrew.InitializeByteBrew();
    });
#else
    ByteBrew.InitializeByteBrew();
#endif
  }
#endif

  void on_level_started(int lvl)
  {
    LionAnalytics.LevelStart(lvl + 1, 0);
  #if HAS_BYTEBREW_SDK
    ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Started, "Level", "", GameState.Progress.Level + 1);
  #endif
  #if HAS_LION_GAME_ANALYTICS_SDK
    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, string.Format("Level_{0:D3}", lvl + 1));
  #endif    
  }
  void on_level_restarted(int lvl)
  {
  #if HAS_LION_GAME_ANALYTICS_SDK
    LionAnalytics.LevelRestart(lvl+ 1, 0);
  #endif 
  }
  void on_level_successed(int lvl)
  {
    on_level_finished(lvl, true);
  }
  void on_level_failed(int lvl)
  {
    on_level_finished(lvl, false);
  }  
  void on_level_finished(int lvl, bool succeed)
  {
    if(succeed)
    {
      LionAnalytics.LevelComplete(lvl + 1, 0);
    #if HAS_BYTEBREW_SDK
      ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Completed, "Level", "", lvl + 1);
    #endif
    #if HAS_LION_GAME_ANALYTICS_SDK
      //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, string.Format("Level_{0:D3}", GameState.Progress.Level + 1));
    #endif  
    }
    else
    {
      LionAnalytics.LevelFail(lvl + 1, 0);
    #if HAS_BYTEBREW_SDK
      ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Failed, "Level", "", lvl + 1);
    #endif
    #if HAS_LION_GAME_ANALYTICS_SDK
      //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, string.Format("Level_{0:D3}", GameState.Progress.Level + 1));
    #endif  
    }
  }  
  void on_invoke_rewarded_ad(string placement)
  {
    // LionAnalytics.RewardVideoShow(placement);
    // var dict = new Dictionary<string, object>()
    // {
    //   {"Placement", placement}
    // };
    // GameAnalyticsSDK.GameAnalytics.NewDesignEvent("RewardedAdShow_" + placement, dict);
  }
  void on_show_rewarded_ad(string placement)
  {
    // LionAnalytics.RewardVideoStart(placement);
    // var dict = new Dictionary<string, object>()
    // {
    //   {"Placement", placement}
    // };
    // GameAnalyticsSDK.GameAnalytics.NewDesignEvent("RewardedAdStart_" + placement, dict);
  }
  void on_show_inters_ad(string placement)
  {
    // var dict = new Dictionary<string, object>()
    // {
    //   {"Placement", placement}
    // };
    // GameAnalyticsSDK.GameAnalytics.NewDesignEvent("IntersAdStart_" + placement, dict);
  }
}
#endif