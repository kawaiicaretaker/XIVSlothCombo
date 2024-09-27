using Dalamud.Game.ClientState.JobGauge.Types;
using ECommons.DalamudServices;
using XIVSlothCombo.Combos.JobHelpers.Enums;
using XIVSlothCombo.Combos.PvE;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Data;

namespace XIVSlothCombo.Combos.JobHelpers
{
    internal class PCTOpenerLogic : PvE.PCT
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
                if (CustomComboFunctions.LocalPlayer.CastActionId == RainbowDrip && PrePullStep == 1) CurrentState = OpenerState.InOpener;
                else if (PrePullStep == 1) actionID = RainbowDrip;

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
                if (Config.PCT_Advanced_OpenerSelection == 0)
                {
                    if (CustomComboFunctions.WasLastAction(StrikingMuse) && OpenerStep == 1) OpenerStep++;
                    else if (OpenerStep == 1) actionID = StrikingMuse;

                    if (CustomComboFunctions.WasLastAction(HolyInWhite) && OpenerStep == 2) OpenerStep++;
                    else if (OpenerStep == 2) actionID = HolyInWhite;

                    if (CustomComboFunctions.WasLastAction(PomMuse) && OpenerStep == 3) OpenerStep++;
                    else if (OpenerStep == 3) actionID = PomMuse;

                    if (CustomComboFunctions.WasLastAction(WingMotif) && OpenerStep == 4) OpenerStep++;
                    else if (OpenerStep == 4) actionID = WingMotif;

                    if (CustomComboFunctions.WasLastAction(StarryMuse) && OpenerStep == 5) OpenerStep++;
                    else if (OpenerStep == 5) actionID = StarryMuse;

                    if (CustomComboFunctions.WasLastAction(HammerStamp) && OpenerStep == 6) OpenerStep++;
                    else if (OpenerStep == 6) actionID = HammerStamp;

                    if (CustomComboFunctions.WasLastAction(WingedMuse) && OpenerStep == 7) OpenerStep++;
                    else if (OpenerStep == 7) actionID = WingedMuse;

                    if (CustomComboFunctions.WasLastAction(HammerBrush) && OpenerStep == 8) OpenerStep++;
                    else if (OpenerStep == 8) actionID = HammerBrush;

                    if (CustomComboFunctions.WasLastAction(MogoftheAges) && OpenerStep == 9) OpenerStep++;
                    else if (OpenerStep == 9) actionID = MogoftheAges;

                    if (CustomComboFunctions.WasLastAction(PolishingHammer) && OpenerStep == 10) OpenerStep++;
                    else if (OpenerStep == 10) actionID = PolishingHammer;

                    if (CustomComboFunctions.WasLastAction(SubtractivePalette) && OpenerStep == 11) OpenerStep++;
                    else if (OpenerStep == 11) actionID = SubtractivePalette;

                    if (CustomComboFunctions.WasLastAction(BlizzardinCyan) && OpenerStep == 12) OpenerStep++;
                    else if (OpenerStep == 12) actionID = BlizzardinCyan;

                    if (CustomComboFunctions.WasLastAction(StoneinYellow) && OpenerStep == 13) OpenerStep++;
                    else if (OpenerStep == 13) actionID = StoneinYellow;

                    if (CustomComboFunctions.WasLastAction(ThunderinMagenta) && OpenerStep == 14) OpenerStep++;
                    else if (OpenerStep == 14) actionID = ThunderinMagenta;

                    if (CustomComboFunctions.WasLastAction(CometinBlack) && OpenerStep == 15) OpenerStep++;
                    else if (OpenerStep == 15) actionID = CometinBlack;

                    if (CustomComboFunctions.WasLastAction(StarPrism) && OpenerStep == 16) OpenerStep++;
                    else if (OpenerStep == 16) actionID = StarPrism;

                    if (CustomComboFunctions.WasLastAction(RainbowDrip) && OpenerStep == 17) CurrentState = OpenerState.OpenerFinished;
                    else if (OpenerStep == 17) actionID = RainbowDrip;

                    if (ActionWatching.TimeSinceLastAction.TotalSeconds >= 5)
                        CurrentState = OpenerState.FailedOpener;
                }

                else
                {
                    if (CustomComboFunctions.WasLastAction(StrikingMuse) && OpenerStep == 1) OpenerStep++;
                    else if (OpenerStep == 1) actionID = StrikingMuse;

                    if (CustomComboFunctions.WasLastAction(HolyInWhite) && OpenerStep == 2) OpenerStep++;
                    else if (OpenerStep == 2) actionID = HolyInWhite;

                    if (CustomComboFunctions.WasLastAction(PomMuse) && OpenerStep == 3) OpenerStep++;
                    else if (OpenerStep == 3) actionID = PomMuse;

                    if (CustomComboFunctions.WasLastAction(WingMotif) && OpenerStep == 4) OpenerStep++;
                    else if (OpenerStep == 4) actionID = WingMotif;

                    if (CustomComboFunctions.WasLastAction(StarryMuse) && OpenerStep == 5) OpenerStep++;
                    else if (OpenerStep == 5) actionID = StarryMuse;

                    if (CustomComboFunctions.WasLastAction(HammerStamp) && OpenerStep == 6) OpenerStep++;
                    else if (OpenerStep == 6) actionID = HammerStamp;

                    if (CustomComboFunctions.WasLastAction(SubtractivePalette) && OpenerStep == 7) OpenerStep++;
                    else if (OpenerStep == 7) actionID = SubtractivePalette;

                    if (CustomComboFunctions.WasLastAction(BlizzardinCyan) && OpenerStep == 8) OpenerStep++;
                    else if (OpenerStep == 8) actionID = BlizzardinCyan;

                    if (CustomComboFunctions.WasLastAction(StoneinYellow) && OpenerStep == 9) OpenerStep++;
                    else if (OpenerStep == 9) actionID = StoneinYellow;

                    if (CustomComboFunctions.WasLastAction(WingedMuse) && OpenerStep == 10) OpenerStep++;
                    else if (OpenerStep == 10) actionID = WingedMuse;

                    if (CustomComboFunctions.WasLastAction(ThunderinMagenta) && OpenerStep == 11) OpenerStep++;
                    else if (OpenerStep == 11) actionID = ThunderinMagenta;

                    if (CustomComboFunctions.WasLastAction(MogoftheAges) && OpenerStep == 12) OpenerStep++;
                    else if (OpenerStep == 12) actionID = MogoftheAges;

                    if (CustomComboFunctions.WasLastAction(CometinBlack) && OpenerStep == 13) OpenerStep++;
                    else if (OpenerStep == 13) actionID = CometinBlack;

                    if (CustomComboFunctions.WasLastAction(All.LucidDreaming) && OpenerStep == 14) OpenerStep++;
                    else if (OpenerStep == 14) actionID = All.LucidDreaming;

                    if (CustomComboFunctions.WasLastAction(StarPrism) && OpenerStep == 15) OpenerStep++;
                    else if (OpenerStep == 15) actionID = StarPrism;

                    if (CustomComboFunctions.WasLastAction(HammerBrush) && OpenerStep == 16) OpenerStep++;
                    else if (OpenerStep == 16) actionID = HammerBrush;

                    if (CustomComboFunctions.WasLastAction(PolishingHammer) && OpenerStep == 17) OpenerStep++;
                    else if (OpenerStep == 17) actionID = PolishingHammer;

                    if (CustomComboFunctions.WasLastAction(RainbowDrip) && OpenerStep == 18) OpenerStep++;
                    else if (OpenerStep == 18) actionID = RainbowDrip;

                    if (CustomComboFunctions.WasLastAction(FireInRed) && OpenerStep == 19) OpenerStep++;
                    else if (OpenerStep == 19) actionID = FireInRed;

                    if (CustomComboFunctions.WasLastAction(All.Swiftcast) && OpenerStep == 20) OpenerStep++;
                    else if (OpenerStep == 20) actionID = All.Swiftcast;

                    if (CustomComboFunctions.WasLastAction(ClawMotif) && OpenerStep == 21) OpenerStep++;
                    else if (OpenerStep == 21) actionID = ClawMotif;

                    if (CustomComboFunctions.WasLastAction(ClawedMuse) && OpenerStep == 22) CurrentState = OpenerState.OpenerFinished;
                    else if (OpenerStep == 22) actionID = ClawedMuse;

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

    internal static class PCTHelper
    {
        public static bool HasWeaponMotif(this PictoGauge gauge) => gauge.HasWeaponMotif();
        public static bool HasCreatureMotif(this PictoGauge gauge) => gauge.HasCreatureMotif();
        public static bool HasSceneMotif(this PictoGauge gauge) => gauge.HasSceneMotif();
    }
}