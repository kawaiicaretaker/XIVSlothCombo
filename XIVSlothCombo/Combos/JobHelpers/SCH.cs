using Dalamud.Game.ClientState.JobGauge.Types;
using ECommons.DalamudServices;
using XIVSlothCombo.Combos.JobHelpers.Enums;
using XIVSlothCombo.Combos.PvE;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Data;

namespace XIVSlothCombo.Combos.JobHelpers
{
    internal class SCHOpenerLogic
    {
        private static bool HasCooldowns()
        {
            return true;
        }

        public static bool HasPrePullCooldowns()
        {
            //if (CustomComboFunctions.GetRemainingCharges(Sharpcast) < 2)
            //return false;

            if (CustomComboFunctions.LocalPlayer.CurrentMp < 10000)
                return false;

            return true;
        }

        private static uint OpenerLevel => 100;

        public uint PrePullStep = 0;

        public uint OpenerStep = 1;

        public static bool LevelChecked => CustomComboFunctions.LocalPlayer.Level >= OpenerLevel;

        private static bool CanOpener => HasCooldowns() && HasPrePullCooldowns() && LevelChecked;

        private OpenerState currentState = OpenerState.PrePull;

        public OpenerState CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                if (value != currentState)
                {
                    if (value == OpenerState.PrePull)
                    {
                        Svc.Log.Debug($"Entered PrePull Opener");
                    }
                    if (value == OpenerState.InOpener) OpenerStep = 1;
                    if (value == OpenerState.OpenerFinished || value == OpenerState.FailedOpener)
                    {
                        if (value == OpenerState.FailedOpener)
                            Svc.Log.Information($"Opener Failed at step {OpenerStep}");

                        ResetOpener();
                    }
                    if (value == OpenerState.OpenerFinished) Svc.Log.Information("Opener Finished");

                    currentState = value;
                }
            }
        }

        private bool DoPrePullSteps(ref uint actionID)
        {
            if (!LevelChecked) return false;

            if (CanOpener && PrePullStep == 0)
            {
                PrePullStep = 1;
            }

            if (!HasCooldowns())
            {
                PrePullStep = 0;
            }

            if (CurrentState == OpenerState.PrePull && PrePullStep > 0)
            {
                if (CustomComboFunctions.LocalPlayer.CastActionId == SCH.Broil4 && PrePullStep == 1) CurrentState = OpenerState.InOpener;
                else if (PrePullStep == 1) actionID = SCH.Broil4;

                if (PrePullStep > 1 && CustomComboFunctions.GetResourceCost(actionID) > CustomComboFunctions.LocalPlayer.CurrentMp && ActionWatching.TimeSinceLastAction.TotalSeconds >= 2)
                    CurrentState = OpenerState.FailedOpener;

                if (ActionWatching.CombatActions.Count > 2 && CustomComboFunctions.InCombat())
                    CurrentState = OpenerState.FailedOpener;

                return true;
            }

            PrePullStep = 0;
            return false;
        }

        private bool DoOpener(ref uint actionID)
        {
            if (!LevelChecked) return false;

            if (currentState == OpenerState.InOpener)
            {
                if (CustomComboFunctions.GetResourceCost(actionID) > CustomComboFunctions.LocalPlayer.CurrentMp && ActionWatching.TimeSinceLastAction.TotalSeconds >= 2)
                {
                    CurrentState = OpenerState.FailedOpener;
                    return false;
                }

                if (true)
                {
                    if (CustomComboFunctions.WasLastAction(SCH.Biolysis) && OpenerStep == 1) OpenerStep++;
                    else if (OpenerStep == 1) actionID = CustomComboFunctions.OriginalHook(SCH.Biolysis);

                    if (CustomComboFunctions.WasLastAction(SCH.Dissipation) && OpenerStep == 2) OpenerStep++;
                    else if (OpenerStep == 2) actionID = CustomComboFunctions.OriginalHook(SCH.Dissipation);

                    if (CustomComboFunctions.WasLastAction(SCH.Broil4) && OpenerStep == 3) OpenerStep++;
                    else if (OpenerStep == 3) actionID = CustomComboFunctions.OriginalHook(SCH.Broil4);

                    if (CustomComboFunctions.WasLastAction(SCH.ChainStratagem) && OpenerStep == 4) OpenerStep++;
                    else if (OpenerStep == 4) actionID = CustomComboFunctions.OriginalHook(SCH.ChainStratagem);

                    if (CustomComboFunctions.WasLastAction(SCH.Broil4) && OpenerStep == 5) OpenerStep++;
                    else if (OpenerStep == 5) actionID = CustomComboFunctions.OriginalHook(SCH.Broil4);

                    if (CustomComboFunctions.WasLastAction(SCH.EnergyDrain) && OpenerStep == 6) OpenerStep++;
                    else if (OpenerStep == 6) actionID = CustomComboFunctions.OriginalHook(SCH.EnergyDrain);

                    if (CustomComboFunctions.WasLastAction(SCH.Broil4) && OpenerStep == 7) OpenerStep++;
                    else if (OpenerStep == 7) actionID = CustomComboFunctions.OriginalHook(SCH.Broil4);

                    if (CustomComboFunctions.WasLastAction(SCH.EnergyDrain) && OpenerStep == 8) OpenerStep++;
                    else if (OpenerStep == 8) actionID = CustomComboFunctions.OriginalHook(SCH.EnergyDrain);

                    if (CustomComboFunctions.WasLastAction(SCH.Broil4) && OpenerStep == 9) OpenerStep++;
                    else if (OpenerStep == 9) actionID = CustomComboFunctions.OriginalHook(SCH.Broil4);

                    if (CustomComboFunctions.WasLastAction(SCH.EnergyDrain) && OpenerStep == 10) OpenerStep++;
                    else if (OpenerStep == 10) actionID = CustomComboFunctions.OriginalHook(SCH.EnergyDrain);

                    if (CustomComboFunctions.WasLastAction(SCH.Broil4) && OpenerStep == 11) OpenerStep++;
                    else if (OpenerStep == 11) actionID = CustomComboFunctions.OriginalHook(SCH.Broil4);

                    if (CustomComboFunctions.WasLastAction(SCH.Aetherflow) && OpenerStep == 12) OpenerStep++;
                    else if (OpenerStep == 12) actionID = CustomComboFunctions.OriginalHook(SCH.Aetherflow);

                    if (CustomComboFunctions.WasLastAction(SCH.Broil4) && OpenerStep == 13) OpenerStep++;
                    else if (OpenerStep == 13) actionID = CustomComboFunctions.OriginalHook(SCH.Broil4);

                    if (CustomComboFunctions.WasLastAction(SCH.BanefulImpaction) && OpenerStep == 14) OpenerStep++;
                    else if (OpenerStep == 14) actionID = CustomComboFunctions.OriginalHook(SCH.BanefulImpaction);

                    if (CustomComboFunctions.WasLastAction(SCH.Broil4) && OpenerStep == 15) OpenerStep++;
                    else if (OpenerStep == 15) actionID = CustomComboFunctions.OriginalHook(SCH.Broil4);

                    if (CustomComboFunctions.WasLastAction(SCH.EnergyDrain) && OpenerStep == 16) OpenerStep++;
                    else if (OpenerStep == 16) actionID = CustomComboFunctions.OriginalHook(SCH.EnergyDrain);

                    if (CustomComboFunctions.WasLastAction(SCH.Broil4) && OpenerStep == 17) OpenerStep++;
                    else if (OpenerStep == 17) actionID = CustomComboFunctions.OriginalHook(SCH.Broil4);

                    if (CustomComboFunctions.WasLastAction(SCH.EnergyDrain) && OpenerStep == 18) OpenerStep++;
                    else if (OpenerStep == 18) actionID = CustomComboFunctions.OriginalHook(SCH.EnergyDrain);

                    if (CustomComboFunctions.WasLastAction(SCH.Broil4) && OpenerStep == 19) OpenerStep++;
                    else if (OpenerStep == 19) actionID = CustomComboFunctions.OriginalHook(SCH.Broil4);

                    if (CustomComboFunctions.WasLastAction(SCH.EnergyDrain) && OpenerStep == 20) CurrentState = OpenerState.OpenerFinished;
                    else if (OpenerStep == 20) actionID = CustomComboFunctions.OriginalHook(SCH.EnergyDrain);

                    if (ActionWatching.TimeSinceLastAction.TotalSeconds >= 5)
                        CurrentState = OpenerState.FailedOpener;
                }
                if (CustomComboFunctions.GetResourceCost(actionID) > CustomComboFunctions.LocalPlayer.CurrentMp && ActionWatching.TimeSinceLastAction.TotalSeconds >= 2)
                {
                    CurrentState = OpenerState.FailedOpener;
                    return false;
                }

                //if (((actionID == All.LucidDreaming && CustomComboFunctions.IsOnCooldown(All.LucidDreaming)) ||
                //    (actionID == All.Swiftcast && CustomComboFunctions.IsOnCooldown(All.Swiftcast))))
                //{
                //    CurrentState = OpenerState.FailedOpener;
                //    return false;
                //}

                return true;
            }

            return false;
        }

        private bool DoOpenerSimple(ref uint actionID)
        {
            if (!LevelChecked) return false;

            if (currentState == OpenerState.InOpener)
            {
                if (ActionWatching.TimeSinceLastAction.TotalSeconds >= 5)
                    CurrentState = OpenerState.FailedOpener;

                if (CustomComboFunctions.GetResourceCost(actionID) > CustomComboFunctions.LocalPlayer.CurrentMp && ActionWatching.TimeSinceLastAction.TotalSeconds >= 2)
                    CurrentState = OpenerState.FailedOpener;

                if ((actionID == All.LucidDreaming && CustomComboFunctions.IsOnCooldown(All.LucidDreaming)) ||
                    (actionID == All.Swiftcast && CustomComboFunctions.IsOnCooldown(All.Swiftcast)))
                {
                    CurrentState = OpenerState.FailedOpener;
                    return false;
                }

                return true;
            }

            return false;
        }

        private void ResetOpener()
        {
            PrePullStep = 0;
            OpenerStep = 0;
        }

        public bool DoFullOpener(ref uint actionID, bool simpleMode)
        {
            if (!LevelChecked) return false;

            if (CurrentState == OpenerState.PrePull)
                if (DoPrePullSteps(ref actionID)) return true;

            if (CurrentState == OpenerState.InOpener)
            {
                if (simpleMode)
                {
                    if (DoOpenerSimple(ref actionID)) return true;
                }
                else
                {
                    if (DoOpener(ref actionID)) return true;
                }
            }

            if (!CustomComboFunctions.InCombat())
            {
                ResetOpener();
                CurrentState = OpenerState.PrePull;
            }


            return false;
        }
    }
}