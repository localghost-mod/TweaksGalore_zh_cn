using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using TweaksGalore;
using UnityEngine;
using Verse;

namespace Tweaks_Galore_zh_cn
{
    [StaticConstructorOnStartup]
    public static class Startup
    {
        static Startup()
        {
            var harmony = new Harmony("localghost.TweaksGalore.zhcn");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(SettingsPage_General), "DoSettings_Vanilla")]
    public class DoSettings_VanillaPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        private static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "爆炸羊流出化学液体",
                "将爆炸羊和爆炸鼠的血液改为化学液体。",
                ref settings.tweak_boomalopesBleedChemfuel
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "通信台聊天",
                "在通信控制台上添加娱乐类型，允许小人通过“聊天”获得娱乐。",
                ref settings.tweak_chattyComms
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "摧毁任何物品",
                "允许在火化炉中熔炼几乎所有物品，除了武器、衣物和打包建筑。当心，不要摧毁重要的东西。",
                ref settings.tweak_destroyAnything
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "禁用窄头",
                "移除窄头，其不能很好地适应许多甚至基础发型和胡须样式，感谢1.4版本将它们定义为很容易删除。",
                ref settings.tweak_disableNarrowHeads
            );
            listing.GapLine();
            listing.CheckboxEnhanced("不带食物", "防止小人携带食物。", ref settings.tweak_dontPackFood);
            listing.GapLine();
            listing.CheckboxEnhanced(
                "更快速的打磨",
                "启用额外的数据以加速对岩石的打磨。默认：300%",
                ref settings.tweak_fasterSmoothing
            );
            if (settings.tweak_fasterSmoothing)
            {
                listing.AddLabeledSlider(
                    "- 打磨速度因子：" + settings.tweak_fasterSmoothing_factor.ToStringPercent(),
                    ref settings.tweak_fasterSmoothing_factor,
                    0.1f,
                    10.0f,
                    "最小：10%",
                    "最大：1000%",
                    0.1f
                );
            }
            listing.GapLine();
            listing.CheckboxEnhanced(
                "完全拆解返还",
                "返还建筑物所需的全部材料。不再因为移动不能卸载的建筑物而受到惩罚。",
                ref settings.tweak_fixDeconstructionReturn
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "发光仙馐果",
                "令仙馐果周围有轻微的发光，使其在黑暗中更容易找到。",
                ref settings.tweak_glowingAmbrosia
            );
            listing.GapLine();
            string healrootName = (settings.tweak_healrootToXerigium ? "药草" : "药草");
            listing.CheckboxEnhanced(
                "发光药草" + healrootName,
                "令" + healrootName + "周围有轻微的发光，使其在黑暗中更容易找到。",
                ref settings.tweak_glowingHealroot
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "可种植仙馐果",
                "允许在种植区域种植仙馐果。",
                ref settings.tweak_growableAmbrosia
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "可种植草",
                "允许在种植区域种植草和高草。如果你安装了生物科技模组，你还可以种植灰草。",
                ref settings.tweak_growableGrass
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "可种植蘑菇",
                "允许在植物区域种植光屎菇和荧蕈，并对速率和产量进行合理的调整。",
                ref settings.tweak_growableMushrooms
            );
            listing.GapLine();
            listing.CheckboxEnhanced("隐藏电线", "视觉上隐藏连接到电缆的电线。", ref settings.tweak_hiddenConduits);
            listing.GapLine();
            listing.CheckboxEnhanced(
                "猎人可以使用近战武器",
                "允许猎人使用近战武器，不管他们的近战能力有多强。",
                ref settings.tweak_hunterMelee
            );
            if (settings.tweak_hunterMelee)
            {
                listing.CheckboxLabeled(
                    "- 允许使用拳头",
                    ref settings.tweak_hunterMelee_fistFighting,
                    "允许猎人使用拳头狩猎，愚蠢但有效。"
                );
            }
            listing.GapLine();
            listing.CheckboxEnhanced(
                "事件角色统计",
                "显示作为事件奖励的任何角色的信息。",
                ref settings.patch_incidentPawnStats
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "阻止虫群生成在坚硬地板上",
                "防止在更坚硬的地板上（金属或石材材料制成的）发生虫群事件。只能防止事件选择在这个位置发生，巢穴仍然可以在附近发生。",
                ref settings.patch_strongFloorsStopInfestations
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "侮辱纵乐削弱",
                "使侮辱纵乐更容易处理。"
                    + "\n- 强度：轻度 --> 中度"
                    + "\n- 最大持续时间：45,000 刻 --> 20,000 刻"
                    + "\n- 最小持续时间：25,000 刻 --> 2,500 刻"
                    + "\n- 基础事件几率：0.5 --> 0.15",
                ref settings.tweak_insultingSpreeNerf
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "无法通过的深水",
                "将深水更改为对角色不可通过，防止它们通过深水行走。",
                ref settings.tweak_impassableDeepWater
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "不卡灯",
                "从燃油灯中移除燃料需求，小幅降低性能影响。",
                ref settings.tweak_lagFreeLamps
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "降低囚犯期望",
                "将囚犯期望降低到更合理的“低期望”水平。他们大多数时候只是试图毁灭你的殖民地，他们不值得有期望。",
                ref settings.patch_lowPrisonerExpectations
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "厌世特质",
                "添加厌世特质（讨厌人类），还使得如果一个角色本应该同时拥有厌男和厌女特质，那么他们将得到厌世特质。",
                ref settings.tweak_misanthropeTrait
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "下一次补给计时器",
                "在世界地图信息中显示定居点是否在你上次访问后重新补给及距离下次补给还有多长时间。",
                ref settings.patch_settlementTraderTimer
            );
            listing.GapLine();
            if (ModLister.GetActiveModWithIdentifier("kentington.saveourship2") == null)
            {
                listing.CheckboxEnhanced(
                    "无故障",
                    "从所有具有此组件的物品中移除故障组件，强制性的资源消耗是懒惰的。",
                    ref settings.tweak_noBreakdowns
                );
                listing.GapLine();
            }
            listing.CheckboxEnhanced(
                "动物猎食人类",
                "设置决定动物是否以猎食人类的形式出现。这并不阻止它们在受伤的情况下发生。",
                ref settings.tweak_noFriendShapedManhunters
            );
            if (settings.tweak_noFriendShapedManhunters)
            {
                listing.AddLabelLine("- 通过可训练能力防止：");
                listing.CheckboxLabeled("-- 中级", ref settings.tweak_NFSMTrainability_Intermediate);
                listing.CheckboxLabeled("-- 高级", ref settings.tweak_NFSMTrainability_Advanced);
                listing.CheckboxLabeled("- 如果可亲近，防止：", ref settings.tweak_NFSMNuzzleHours);
                listing.AddLabeledSlider(
                    "- 如果野性低于： " + settings.tweak_NFSMWildness.ToStringPercent(),
                    ref settings.tweak_NFSMWildness,
                    0f,
                    1f,
                    "最小：0%",
                    "最大：100%",
                    0.01f
                );
                listing.AddLabeledSlider(
                    "- 如果战斗力低于： " + settings.tweak_NFSMCombatPower,
                    ref settings.tweak_NFSMCombatPower,
                    0f,
                    200f,
                    "最小：0",
                    "最大：200",
                    1f
                );
                listing.CheckboxLabeled(
                    "- 在驯服失败时禁用猎食人类",
                    ref settings.tweak_NFSMDisableManhunterOnTame,
                    "使用相同的参数阻止动物在驯服失败时发狂猎，如果它们受伤仍然可以发狂猎。"
                );
            }
            listing.GapLine();
            listing.CheckboxEnhanced("不那么野生的浆果", "使浆果可以被种植。", ref settings.tweak_notSoWildBerries);
            listing.GapLine();
            listing.CheckboxEnhanced(
                "囚犯没有钥匙",
                "有选择地控制囚犯和奴隶在爆发和叛乱期间是否能够打开门。",
                ref settings.patch_prisonersDontHaveKeys
            );
            if (settings.patch_prisonersDontHaveKeys)
            {
                listing.CheckboxLabeled("- 应用于囚犯", ref settings.patch_pdhk_prisoners);
                listing.CheckboxLabeled("- 应用于奴隶", ref settings.patch_pdhk_slaves);
                listing.CheckboxLabeled("- 逃脱的角色可以打开自己的门", ref settings.patch_pdhk_ownDoor);
            }
            listing.GapLine();
            listing.CheckboxEnhanced(
                "熟练的石材切割",
                "使石材切割提高工艺技能，并使更有经验的工匠更快地制作石块。",
                ref settings.tweak_skilledStonecutting
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "技能速率调整",
                "启用对技能增益/减益速率以及应该发生在何种级别的更多控制。",
                ref settings.tweak_skillRates
            );
            if (settings.tweak_skillRates)
            {
                listing.AddLabeledSlider(
                    "- 技能减益因子：" + settings.tweak_skillRateLoss.ToStringPercent(),
                    ref settings.tweak_skillRateLoss,
                    0f,
                    2f,
                    "最小：0%",
                    "最大：200%",
                    0.01f
                );
                listing.AddLabeledSlider(
                    "- 技能增益因子：" + settings.tweak_skillRateGain.ToStringPercent(),
                    ref settings.tweak_skillRateGain,
                    0f,
                    2f,
                    "最小：0%",
                    "最大：200%",
                    0.01f
                );
                listing.AddLabeledSlider(
                    "- 技能减益阈值：" + settings.tweak_skillRateLossThreshold.ToString(),
                    ref settings.tweak_skillRateLossThreshold,
                    0f,
                    20f,
                    "最小：0",
                    "最大：20",
                    1f
                );
                listing.Note("技能减益阈值是技能必须达到的水平，然后技能减益开始发生，它将在该水平以下完全阻止技能减益。", GameFont.Tiny);
            }
            listing.GapLine();
            listing.CheckboxEnhanced(
                "边缘瘦身",
                "允许在生成角色时禁用身体类型。这只在生成角色时生效，不会影响现有角色。替代身体将是男性或女性，与角色的性别匹配。",
                ref settings.patch_slimRim
            );
            if (settings.patch_slimRim)
            {
                listing.CheckboxLabeled("- 胖", ref settings.patch_slimRim_fat);
                listing.CheckboxLabeled("- 巨", ref settings.patch_slimRim_hulk);
                listing.CheckboxLabeled("- 瘦", ref settings.patch_slimRim_thin);
            }
            listing.GapLine();
            listing.CheckboxEnhanced(
                "特质数调整",
                "截至1.4版本，这还没有完全实现，特质生成方式的变化使得前1-3个特质总是可以正常生成，不管我做什么。所以目前只对那些想要保证多于1个或总体上想要更多特质的人有用。不会影响新生儿。",
                ref settings.tweak_traitCountAdjustment
            );
            if (settings.tweak_traitCountAdjustment)
            {
                listing.Label("- 特质数范围");
                listing.Note(
                    $"当前：{settings.tweak_traitCountRange.min}-{settings.tweak_traitCountRange.max}  最小：1  最大：8",
                    GameFont.Tiny
                );
                listing.IntRange(ref settings.tweak_traitCountRange, 1, 8);
            }
            listing.GapLine();
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Anima), "DoSettings_Anima")]
    public class DoSettings_AnimaPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "启用仙树调整",
                "默认情况下，出于兼容性考虑，整个部分都是禁用的，主要是因为一些与仙树相关的调整无论是否已更改（例如更改半径）都会生效，因此如果您更喜欢使用这些调整而没有此设置，可能会导致与其他仙树相关的mod的兼容性问题。",
                ref settings.tweak_animaTweaks
            );
            if (settings.tweak_animaTweaks)
            {
                listing.GapLine();
                listing.CheckboxEnhanced(
                    "可移植的仙树",
                    "使仙树可以像其他任何树一样移动。",
                    ref settings.tweak_replantableAnima
                );
                listing.GapLine();
                listing.CheckboxEnhanced(
                    "禁用仙树尖叫",
                    "禁用从砍伐它们时产生的仙树尖叫debuff。",
                    ref settings.tweak_animaDisableScream
                );
                listing.GapLine();
                if (!settings.tweak_animaDisableScream)
                {
                    listing.AddLabeledSlider(
                        $"- 尖叫debuff：{settings.tweak_animaScreamDebuff.ToString("0")}",
                        ref settings.tweak_animaScreamDebuff,
                        -20f,
                        20f,
                        "最小：-20",
                        "最大：20",
                        1f
                    );
                    listing.AddLabeledSlider(
                        $"- 尖叫持续时间：{settings.tweak_animaScreamLength.ToString("0.0")}",
                        ref settings.tweak_animaScreamLength,
                        0.1f,
                        20f,
                        "最小：0.1",
                        "最大：20",
                        0.1f
                    );
                    listing.AddLabeledSlider(
                        $"- 尖叫堆叠限制：{settings.tweak_animaScreamStackLimit.ToString("0")}",
                        ref settings.tweak_animaScreamStackLimit,
                        1f,
                        20f,
                        "最小：1",
                        "最大：20",
                        1f
                    );
                    listing.GapLine();
                }
                listing.Label("启灵级别的仙草");
                {
                    listing.Note("这些值是需要在上一级别的基础上种植的草的数量。", GameFont.Tiny, Color.gray);
                    if (SettingsPage_Anima.GetPsylinkStuff)
                    {
                        int levelint = 0;
                        for (int i = 0; i < settings.tweak_animaPsylinkLevelNeeds.Count; i++)
                        {
                            levelint++;
                            string intBufferString = settings
                                .tweak_animaPsylinkLevelNeeds[i]
                                .ToString();
                            int intBufferInt = settings.tweak_animaPsylinkLevelNeeds[i];
                            listing.TextFieldNumericLabeled(
                                "Psylink Level " + levelint,
                                ref intBufferInt,
                                ref intBufferString,
                                0,
                                500
                            );
                            settings.tweak_animaPsylinkLevelNeeds[i] = intBufferInt;
                        }
                    }
                }
                listing.GapLine();
                listing.CheckboxEnhanced(
                    "自然神殿始终可建造",
                    "通常只有当您拥有基于自然的灵能使者时，才能建造自然神殿，这解锁了该限制。",
                    ref settings.tweak_animaBuildableShrines
                );
                listing.GapLine();
                listing.Label("树调整");
                listing.Note("这些是特定于仙树本身的。", GameFont.Tiny, Color.gray);
                listing.GapLine();
                listing.AddLabeledSlider(
                    $"- 人工建筑半径：{settings.tweak_animaArtificialBuildingRadius.ToString("0.0")}",
                    ref settings.tweak_animaArtificialBuildingRadius,
                    0.1f,
                    40.9f,
                    "最小：0.1",
                    "最大：40.9",
                    0.1f
                );
                listing.Note("在建筑人工建筑物的半径内，对树的效果施加debuff。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 自然建筑半径：{settings.tweak_animaBuffBuildingRadius.ToString("0.0")}",
                    ref settings.tweak_animaBuffBuildingRadius,
                    0.1f,
                    40.9f,
                    "最小：0.1",
                    "最大：40.9",
                    0.1f
                );
                listing.Note("对于自然建筑物，施加debuff的半径。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 最大自然建筑物数：{settings.tweak_animaMaxBuffBuildings}",
                    ref settings.tweak_animaMaxBuffBuildings,
                    1f,
                    40f,
                    "最小：1",
                    "最大：40",
                    1f
                );
                listing.Note("可以提升仙树的建筑物的最大数量。", GameFont.Tiny, Color.gray);
                listing.GapLine();
                listing.Label("神殿调整");
                listing.Note("这些是特定于可以建造以增强仙树的自然神殿的。", GameFont.Tiny, Color.gray);
                listing.GapLine();
                listing.AddLabeledSlider(
                    $"- 人工建筑半径：{settings.tweak_animaShrineBuildingRadius.ToString("0.0")}",
                    ref settings.tweak_animaShrineBuildingRadius,
                    0.1f,
                    40.9f,
                    "最小：0.1",
                    "最大：40.9",
                    0.1f
                );
                listing.Note("在建筑人工建筑物的半径内，对树的效果施加debuff。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 自然建筑半径：{settings.tweak_animaShrineBuffBuildingRadius.ToString("0.0")}",
                    ref settings.tweak_animaShrineBuffBuildingRadius,
                    0.1f,
                    40.9f,
                    "最小：0.1",
                    "最大：40.9",
                    0.1f
                );
                listing.Note("对于自然建筑物，施加debuff的半径。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 沉思Psyfocus获得速率：{settings.tweak_animaMeditationGain.ToString("0.0")}",
                    ref settings.tweak_animaMeditationGain,
                    0.1f,
                    20f,
                    "最小：0.1",
                    "最大：20",
                    0.1f
                );
                listing.Note("每天冥想的Psyfocus增益量。", GameFont.Tiny, Color.gray);
                listing.GapLine();
            }
            TweaksGaloreStartup.Tweak_AnimaTweaks(settings);
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Biotech), "DoSettings_Biotech")]
    public class DoSettings_BiotechPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "古代拆解",
                "使由意识形态和生物科技添加的古代废墟可拆卸，而不是必须摧毁它们。",
                ref settings.tweak_ancientDeconstruction
            );
            if (settings.tweak_ancientDeconstruction)
            {
                listing.CheckboxEnhanced(
                    "- 给予适当的材料",
                    "将返回的物品更改为一些合理的材料，而不仅仅是钢渣。",
                    ref settings.tweak_ancientDeconstruction_mode
                );
            }
            listing.GapLine();
            listing.Label("基因");
            listing.GapLine();
            listing.CheckboxEnhanced(
                "简化复杂度",
                "从基因中删除复杂度，使它们都设置为0。",
                ref settings.tweak_flattenComplexity
            );
            listing.CheckboxEnhanced(
                "简化代谢",
                "从基因中删除代谢率，使它们都设置为0。",
                ref settings.tweak_flattenMetabolism
            );
            listing.CheckboxEnhanced(
                "简化超凡",
                "从基因中删除超凡，使它们都设置为0。",
                ref settings.tweak_flattenArchites
            );
            listing.CheckboxEnhanced(
                "显示基因选项卡",
                "默认情况下，角色的基因选项卡是隐藏的，如果您没有其他插件揭示它，您可以使用此选项来显示它。请记住，这在原始游戏中可能被隐藏有原因。",
                ref settings.tweak_showGenesTab
            );
            listing.GapLine();
            listing.AddLabeledSlider(
                $"生成怀孕概率：{settings.tweak_defaultPregnancyChance.ToStringPercent()}",
                ref settings.tweak_defaultPregnancyChance,
                0f,
                1f,
                "最小：0%",
                "最大：100%",
                0.01f
            );
            listing.Note(
                "如果启用了儿童功能，PawnKinds通常有3%的概率在怀孕时生成，该概率由Pawn的生育能力乘以，这使您可以更改该基本概率。不使用默认值的任何PawnKind都将被跳过，以避免引起问题。",
                GameFont.Tiny,
                Color.gray
            );
            TweaksGaloreStartup.UpdatePregnancyChances(settings);
            listing.GapLine();
            listing.Gap();
            listing.CheckboxEnhanced(
                "启用玩家机械族调整",
                "这控制了玩家机械族调整是否尝试运行，某些调整没有其他方式知道它们是否应该运行。",
                ref settings.tweak_playerMechTweaks
            );
            if (settings.tweak_playerMechTweaks)
            {
                listing.AddLabeledSlider(
                    $"- 耗电速率：{settings.tweak_mechanoidDrainRate.ToStringPercent()}",
                    ref settings.tweak_mechanoidDrainRate,
                    0.05f,
                    5f,
                    "最小：5%",
                    "最大：500%",
                    0.05f
                );
                TweaksGaloreStartup.Tweak_PlayerMechTweaks(settings);
            }
            listing.GapLine();
            listing.CheckboxEnhanced(
                "启用机械族调整",
                "这控制了机械族调整是否尝试运行，某些调整没有其他方式知道它们是否应该运行。",
                ref settings.tweak_mechanitorTweaks
            );
            if (settings.tweak_mechanitorTweaks)
            {
                listing.CheckboxEnhanced(
                    "- 禁用范围限制",
                    "使机械族可以控制而不考虑与机械师的距离。如果他们能设计出神经接口，他们就能设计出比便宜的WiFi范围更好的东西。",
                    ref settings.tweak_mechanitorDisableRange
                );
                listing.AddLabeledSlider(
                    $"- 带宽基础：{settings.tweak_mechanitorBandwidthBase}",
                    ref settings.tweak_mechanitorBandwidthBase,
                    1f,
                    30f,
                    "最小：1",
                    "最大：30",
                    1f
                );
                listing.AddLabeledSlider(
                    $"- 控制组基础：{settings.tweak_mechanitorControlGroupBase}",
                    ref settings.tweak_mechanitorControlGroupBase,
                    1f,
                    10f,
                    "最小：1",
                    "最大：10",
                    1f
                );
                listing.AddLabeledSlider(
                    $"- 每个波段节点的带宽：{settings.tweak_mechanitorBandNodeBandwidth}",
                    ref settings.tweak_mechanitorBandNodeBandwidth,
                    1f,
                    20f,
                    "最小：1",
                    "最大：20",
                    1f
                );
                TweaksGaloreStartup.Tweak_MechanitorTweaks(settings);
            }
            listing.GapLine();
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Gauranlen), "DoSettings_Gauranlen")]
    public class DoSettings_GauranlenPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "启用母树调整",
                "为了兼容性，此整个部分默认情况下是禁用的，主要是为了兼容性考虑，一些母树相关的调整功能，无论是否已更改（例如更改半径），因此如果不启用此设置，可能会导致与其他母树相关的模组的兼容性问题，如果你更喜欢使用那些模组。",
                ref settings.tweak_gauranlenTweaks
            );
            if (settings.tweak_gauranlenTweaks)
            {
                listing.CheckboxEnhanced(
                    "可移植的母树",
                    "使您可以像其他树一样移动母树。",
                    ref settings.tweak_replantableGauranlen
                );
                listing.GapLine();
                listing.Label("树木调整");
                listing.GapLine();
                listing.Label("初始连接范围", tooltip: "初始连接强度将在此范围内。");
                listing.AddLabeledSlider(
                    $"- 最小：{settings.tweak_gauranlenInitialConnectionStrength.min.ToStringPercent()}",
                    ref settings.tweak_gauranlenInitialConnectionStrength.min,
                    0f,
                    1f,
                    "最小：0%",
                    "最大：100%",
                    0.01f
                );
                listing.AddLabeledSlider(
                    $"- 最大：{settings.tweak_gauranlenInitialConnectionStrength.max.ToStringPercent()}",
                    ref settings.tweak_gauranlenInitialConnectionStrength.max,
                    0f,
                    1f,
                    "最小：0%",
                    "最大：100%",
                    0.01f
                );
                if (
                    settings.tweak_gauranlenInitialConnectionStrength.min
                    > settings.tweak_gauranlenInitialConnectionStrength.max
                )
                {
                    settings.tweak_gauranlenInitialConnectionStrength.max = settings
                        .tweak_gauranlenInitialConnectionStrength
                        .min;
                }
                listing.Note("这些是特定于母树本身的。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 人工建筑半径：{settings.tweak_gauranlenArtificialBuildingRadius.ToString("0.0")}",
                    ref settings.tweak_gauranlenArtificialBuildingRadius,
                    0.1f,
                    40.9f,
                    "最小：0.1",
                    "最大：40.9",
                    0.1f
                );
                listing.Note("在其中建造人工建筑会对树木产生负面影响的半径。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 修剪时的连接增益：{settings.tweak_gauranlenConnectionGainPerHourPruning.ToStringPercent()}",
                    ref settings.tweak_gauranlenConnectionGainPerHourPruning,
                    0.01f,
                    1f,
                    "最小：1%",
                    "最大：100%",
                    0.01f
                );
                listing.Note("修剪每小时获得的连接。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 连接损失率：{settings.tweak_gauranlenConnectionLossPerLevel.ToStringPercent()}",
                    ref settings.tweak_gauranlenConnectionLossPerLevel,
                    0f,
                    2f,
                    "最小：0%",
                    "最大：200%",
                    0.01f
                );
                listing.Note("这是连接每级连接的连接损失的倍增器。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 每栋建筑的连接损失：{settings.tweak_gauranlenLossPerBuilding.ToStringPercent()}",
                    ref settings.tweak_gauranlenLossPerBuilding,
                    0f,
                    2f,
                    "最小：0%",
                    "最大：200%",
                    0.01f
                );
                listing.Note("这是范围内每个人工建筑的连接损失的倍增器。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 苔藓生长半径：{settings.tweak_gauranlenPlantGrowthRadius.ToString("0.0")}",
                    ref settings.tweak_gauranlenPlantGrowthRadius,
                    0.1f,
                    40.9f,
                    "最小：0.1",
                    "最大：40.9",
                    0.1f
                );
                listing.Note("树木上长苔藓的最大半径。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 苔藓生长天数：{settings.tweak_gauranlenMossGrowDays.ToString("0")}",
                    ref settings.tweak_gauranlenMossGrowDays,
                    1f,
                    30f,
                    "最小：1",
                    "最大：30",
                    1f
                );
                listing.Note("Gauralen Moss成熟需要的天数。", GameFont.Tiny, Color.gray);
                listing.GapLine();
                listing.Label("树妖调整");
                listing.Note("这些是特定于由母树生成的树妖的。", GameFont.Tiny, Color.gray);
                listing.GapLine();
                listing.AddLabeledSlider(
                    $"- 最大树妖数：{settings.tweak_gauranlenMaxDryads.ToString("0")}",
                    ref settings.tweak_gauranlenMaxDryads,
                    3f,
                    30f,
                    "最小：3",
                    "最大：30",
                    1f
                );
                listing.Note(
                    "这通常是一个带有多个点的曲线，这很好。 但我的大脑非常光滑，所以这是一个附加到末尾的固定值，正常值适用于连接达到75%以上，然后它使用您设置的任何值。",
                    GameFont.Tiny,
                    Color.gray
                );
                listing.AddLabeledSlider(
                    $"- 树妖生成天数：{settings.tweak_gauranlenDryadSpawnDays.ToString("0")}",
                    ref settings.tweak_gauranlenDryadSpawnDays,
                    1f,
                    30f,
                    "最小：1",
                    "最大：30",
                    1f
                );
                listing.Note("生成树妖之间的天数。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 树妖茧完成天数：{settings.tweak_gauranlenCocoonDaysToComplete.ToString("0")}",
                    ref settings.tweak_gauranlenCocoonDaysToComplete,
                    1f,
                    30f,
                    "最小：1",
                    "最大：30",
                    1f
                );
                listing.Note("树妖完成茧过程所需的天数。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- Gaumaker Pod天数：{settings.tweak_gauranlenMossGrowDays.ToString("0")}",
                    ref settings.tweak_gauranlenMossGrowDays,
                    1f,
                    30f,
                    "最小：1",
                    "最大：30",
                    1f
                );
                listing.Note("Gaumaker Pod成熟需要的天数。", GameFont.Tiny, Color.gray);
                listing.AddLabeledSlider(
                    $"- 种子产量：{settings.tweak_gauranlenPodHarvestYield.ToString("0")}",
                    ref settings.tweak_gauranlenPodHarvestYield,
                    1f,
                    10f,
                    "最小：1",
                    "最大：10",
                    1f
                );
                listing.Note("从Gaumaker Pod中获得的种子数量。", GameFont.Tiny, Color.gray);
                listing.GapLine();
                TweaksGaloreStartup.Tweak_GauranlenTweaks(settings);
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Ideology), "DoSettings_Ideology")]
    public class DoSettings_IdeologyPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "古代废墟拆除",
                "使由Ideology和Biotech添加的古代废墟可以拆除，而不必摧毁它们。",
                ref settings.tweak_ancientDeconstruction
            );
            if (settings.tweak_ancientDeconstruction)
            {
                listing.CheckboxEnhanced(
                    "- 提供适当的材料",
                    "将返回的物品更改为一些合理的材料，而不仅仅是钢渣块。",
                    ref settings.tweak_ancientDeconstruction_mode
                );
            }
            listing.GapLine();
            listing.CheckboxEnhanced(
                "暗光发光荚",
                "使昆虫巢中生成的发光荚使用暗光颜色。",
                ref settings.tweak_darklightGlowPods
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "禁用所需的服装",
                "禁止默认的意识形态所需的服装显示。当我测试派系装备时，我真的很讨厌这种情况发生，那些没有想到要有一个选项来禁用它的人应该踩在一个竖立的英国电器插头上。",
                ref settings.tweak_disableDesiredApparel
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "无模因限制",
                "将您可以选择的模因数量上限提高到1000...因此实际上没有限制。",
                ref settings.patch_noMemeLimit
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "适当的压制",
                "略微更改压制机制，以便如果您的奴隶保持被压制，就永远不会发生叛乱。还防止被压制的角色在叛乱发生时参与其中。",
                ref settings.patch_properSuppression
            );
            listing.GapLine();
            listing.AddLabeledSlider(
                $"压制百分比：{settings.patch_properSuppressionPercentage.ToStringPercent()}",
                ref settings.patch_properSuppressionPercentage,
                0f,
                1f,
                "最小：0%",
                "最大：100%",
                0.01f
            );
            listing.Note(
                "控制开始镇压的叛乱百分比，如果启用了Proper Suppression，则这是完全禁用叛乱的阈值。",
                GameFont.Tiny,
                Color.gray
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "解锁文化建筑",
                "删除对文化建筑的模因限制，因此您可以无论拥有哪些模因都可以使用它们。包括地板和服装。",
                ref settings.tweak_unlockedIdeologyBuildings
            );
            listing.GapLine();
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Mechanoids), "DoSettings_Mechanoids")]
    public class DoSettings_MechanoidsPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "禁用自适应",
                "禁用机械体适应EMP的能力。",
                ref settings.patch_disableMechanoidAdapting
            );
            listing.GapLine();
            listing.Label("机械体热装甲：" + settings.tweak_mechanoidHeatArmour.ToStringPercent());
            listing.AddLabeledSlider(
                null,
                ref settings.tweak_mechanoidHeatArmour,
                0f,
                2f,
                "最小：0%",
                "最大：200%",
                0.01f
            );
            listing.CheckboxEnhanced(
                "1.0版本之前的船体零件",
                "将心灵和枯萎飞船零件恢复到它们B18状态，其中它们实际上提供了合理的奖励。"
                    + "\n心灵飞船零件残余："
                    + "\n- 钢 x100"
                    + "\n- 玻璃钢 x35"
                    + "\n- 钢渣 x8"
                    + "\n- 工业组件 x4"
                    + "\n- 太空组件 x1"
                    + "\n- 人工智能人格核心"
                    + "\n\n枯萎飞船零件残余："
                    + "\n- 钢 x100"
                    + "\n- 玻璃钢 x35"
                    + "\n- 钢渣 x8"
                    + "\n- 工业组件 x4"
                    + "\n- 太空组件 x1"
                    + "\n- 光辉科技药物 x5",
                ref settings.tweak_preReleaseShipParts
            );
            listing.GapLine();
            if (ModLister.RoyaltyInstalled)
            {
                listing.CheckboxEnhanced(
                    "更好的阴影之光",
                    "更改机械体集群中经常出现的阴影之光，使其产生更多的光，因此它实际上是有用的。",
                    ref settings.tweak_betterGloomlight
                );
                listing.CheckboxEnhanced(
                    "- 日光灯阴影之光",
                    "启用更好的阴影之光并启用此选项后，它们将产生足够的光，以与自由日光灯相等。",
                    ref settings.tweak_gloomlightSunlamp
                );
                listing.CheckboxEnhanced(
                    "- 暗光阴影之光",
                    "更改阴影之光的颜色以匹配'Darklight'的颜色。",
                    ref settings.tweak_gloomlightDarklight
                );
                listing.GapLine();
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Polux), "DoSettings_Polux")]
    public class DoSettings_PoluxPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "启用粹树调整",
                "出于兼容性考虑，此整个部分默认情况下被禁用，主要是因为一些粹树相关的调整功能不受其是否被更改的影响（例如更改半径），因此如果您更喜欢使用这些调整中的其他粹树相关的mod，没有此设置可能会导致兼容性问题。",
                ref settings.tweak_poluxTweaks
            );
            if (settings.tweak_poluxTweaks)
            {
                listing.CheckboxEnhanced(
                    "可移植粹树",
                    "使您可以像其他树木一样移动粹树。",
                    ref settings.tweak_replantablePolux
                );
                listing.GapLine();
                listing.Label("树木调整");
                listing.GapLine();
                listing.AddLabeledSlider(
                    $"- 有效半径：{settings.tweak_poluxEffectRadius.ToString("0.0")}",
                    ref settings.tweak_poluxEffectRadius,
                    0.1f,
                    42f,
                    "最小：0.1",
                    "最大：42",
                    0.1f
                );
                listing.Note("较低的值会增加污染清除效果的速度。", GameFont.Tiny);
                listing.GapLine();
                listing.AddLabeledSlider(
                    $"- 作用速率因子：{settings.tweak_poluxEffectRate.ToStringPercent()}",
                    ref settings.tweak_poluxEffectRate,
                    0.01f,
                    2f,
                    "最小：1%",
                    "最大：200%",
                    0.01f
                );
                listing.Note("较低的值会增加污染清除效果的速度。", GameFont.Tiny);
                listing.GapLine();
                listing.CheckboxEnhanced(
                    "- 忽略人工建筑",
                    "通常情况下，原版游戏会在树木半径内存在人工建筑时禁用树木的效果，这将禁用该行为并使其仍能正常工作。",
                    ref settings.tweak_poluxArtificialDisables
                );
                listing.GapLine();
                TweaksGaloreStartup.Tweak_PoluxTweaks(settings);
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Power), "DoSettings_Power")]
    public class DoSettings_PowerPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "电力使用调整",
                "允许调整某些建筑的使用速率。如果它们通常产生电力，则配置它们的输出量。",
                ref settings.tweak_powerUsageTweaks
            );
            if (settings.tweak_powerUsageTweaks)
            {
                listing.AddLabeledSlider(
                    "- 站立灯: " + settings.tweak_powerUsage_lamp,
                    ref settings.tweak_powerUsage_lamp,
                    0f,
                    100f,
                    "最小：0",
                    "最大：100",
                    1f
                );
                listing.AddLabeledSlider(
                    "- 太阳灯: " + settings.tweak_powerUsage_sunlamp,
                    ref settings.tweak_powerUsage_sunlamp,
                    0f,
                    10000f,
                    "最小：0",
                    "最大：10000",
                    50f
                );
                listing.AddLabeledSlider(
                    "- 自动门: " + settings.tweak_powerUsage_autodoor,
                    ref settings.tweak_powerUsage_autodoor,
                    0f,
                    100f,
                    "最小：0",
                    "最大：100",
                    1f
                );
                listing.AddLabeledSlider(
                    "- 虚空电池: " + settings.tweak_powerUsage_vanometricCell,
                    ref settings.tweak_powerUsage_vanometricCell,
                    0f,
                    10000f,
                    "最小：0",
                    "最大：10000",
                    50f
                );
            }
            listing.GapLine();
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Raids), "DoSettings_Raids")]
    public class DoSettings_RaidsPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "不再有破墙袭击",
                "移除破墙袭击作为袭击者的选项。",
                ref settings.tweak_noMoreBreachRaids
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "不再有空投袭击",
                "移除空投袭击作为袭击者的选项。不适用于RimWar。",
                ref settings.tweak_noMoreDropPodRaids
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "不再有工兵袭击",
                "移除工兵袭击作为袭击者的选项。",
                ref settings.tweak_noMoreSapperRaids
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "不再有围攻袭击",
                "移除围攻袭击作为袭击者的选项。",
                ref settings.tweak_noMoreSiegeRaids
            );
            listing.GapLine();
            listing.CheckboxEnhanced("不再有懦弱袭击", "防止袭击者逃跑。", ref settings.tweak_noCowardlyRaiders);
            listing.GapLine();
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Resources), "DoSettings_Resources")]
    public class DoSettings_ResourcesPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "金属不燃烧",
                "移除钢铁和玻璃钢的易燃性，将金和银的易燃性设置为略高于0，以使它们仍有融化的可能性。",
                ref settings.tweak_metalDoesntBurn
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "无可采掘的组件",
                "从地图生成中移除可采掘的组件。",
                ref settings.tweak_noMineableComponents
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "无可采掘的玻璃钢",
                "从地图生成中移除可采掘的玻璃钢。",
                ref settings.tweak_noMineablePlasteel
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "漂亮的贵金属",
                "将金和银（作为物品，而不是材料）的美观度从-4更改为4。",
                ref settings.tweak_prettyPreciousMetals
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "可堆叠的石块",
                "允许石块和钢渣块堆叠。默认：5",
                ref settings.tweak_stackableChunks
            );
            if (settings.tweak_stackableChunks)
            {
                listing.AddLabeledSlider(
                    "- 石块堆叠大小: " + settings.tweak_stackableChunks_stone,
                    ref settings.tweak_stackableChunks_stone,
                    1f,
                    400f,
                    "最小: 1",
                    "最大: 400",
                    1f
                );
                listing.AddLabeledSlider(
                    "- 钢渣块叠大小: " + settings.tweak_stackableChunks_slag,
                    ref settings.tweak_stackableChunks_slag,
                    1f,
                    400f,
                    "最小: 1",
                    "最大: 400",
                    1f
                );
            }
            listing.GapLine();
            listing.CheckboxEnhanced("坚固的钢铁", "将钢铁作为一种材料的耐久性翻倍。", ref settings.tweak_strongerSteel);
            listing.GapLine();
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Royalty), "DoSettings_Royalty")]
    public class DoSettings_RoyaltyPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "允许谒见厅中的任何建筑物",
                "如果启用，将删除谒见厅中的建筑物限制。包括祭坛。",
                ref settings.tweak_throneroomAnyBuildings
            );
            listing.GapLine();
            if (!settings.tweak_throneroomAnyBuildings)
            {
                listing.CheckboxEnhanced(
                    "允许在谒见厅中放置祭坛",
                    "如果启用，将专门允许在谒见厅中建造祭坛。",
                    ref settings.tweak_throneroomAllowAltars
                );
                listing.GapLine();
            }
            listing.CheckboxEnhanced(
                "延迟的皇室",
                "将皇室的起始任务延迟几天，比通常多一些时间来为其做好准备。",
                ref settings.tweak_delayedRoyalty
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "自由冥想",
                "允许任何角色使用任何冥想焦点建筑物，无论他们拥有什么焦点类型。会自动修补所有原版和修改过的冥想来源。",
                ref settings.tweak_animaMeditationAll
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "无背景故事限制",
                "允许任何角色使用自然或艺术冥想焦点类型，而不受背景故事的限制。如果启用了原版心灵能力扩展，这也将处理解锁心灵能力路径。",
                ref settings.tweak_animaRemoveBackstoryLimits
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "无贸易许可证",
                "如果启用，将无需许可证即可与破碎帝国进行贸易。",
                ref settings.tweak_noTradingPermit
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "可卸下的机械护盾",
                "如果启用，允许您卸下机械集群的护盾供您使用。这还防止在清除集群后关闭护盾，使其保持有用。",
                ref settings.tweak_uninstallableMechShields
            );
            listing.GapLine();
            listing.CheckboxEnhanced(
                "等等，这更好",
                "为所有皇家接受的服装（包括修改的）添加了以前的等级标签，使更高等级的服装可以满足寻找较低等级服装的角色。这真的应该是默认的。",
                ref settings.tweak_waitThisIsBetter
            );
            listing.GapLine();
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_PennedAnimals), "DoSettings_PennedAnimals")]
    public class DoSettings_PennedAnimalsPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "围栏动物设置",
                "控制可圈养性和游荡天数",
                ref settings.tweak_pennedAnimalConfig
            );
            if (settings.tweak_pennedAnimalConfig)
            {
                listing.GapLine();
                for (int i = 0; i < SettingsPage_PennedAnimals.CachedAnimalListing.Count; i++)
                {
                    ThingDef curAnimal = SettingsPage_PennedAnimals.CachedAnimalListing[i];
                    float value = settings.tweak_pennedAnimalDict[curAnimal.defName];
                    listing.AddLabeledSlider(
                        curAnimal.LabelCap + ": " + (value == 0f ? "不可圈养" : (value + "天")),
                        ref value,
                        0f,
                        20f,
                        "禁用",
                        "20天"
                    );
                    settings.tweak_pennedAnimalDict[curAnimal.defName] = value;
                }
                TweaksGaloreStartup.SetPennedAnimals(settings);
            }
            listing.GapLine();
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_RoyaltyTitles), "DoTitleSettings")]
    public class DoTitleSettingsPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing, RoyalTitleDef title)
        {
            listing.Label(title.LabelCap);
            listing.GapLine();
            listing.Note($"标签: {title.tags.ToCommaList()}", GameFont.Tiny, Color.gray);
            float favorCostBuffer = settings.tweak_royalTitleSettings[title.defName].favorCost;
            listing.AddLabeledSlider(
                $"- 声望花费: {favorCostBuffer.ToString("0")}",
                ref favorCostBuffer,
                1f,
                350f,
                "最小: 1",
                "最大: 350",
                1f
            );
            settings.tweak_royalTitleSettings[title.defName].favorCost = favorCostBuffer;
            float heirCostBuffer = settings.tweak_royalTitleSettings[title.defName].heirQuestPoints;
            listing.AddLabeledSlider(
                $"- 继承者任务点数: {heirCostBuffer.ToString("0")}",
                ref heirCostBuffer,
                100f,
                6000f,
                "最小: 100",
                "最大: 6000",
                100f
            );
            settings.tweak_royalTitleSettings[title.defName].heirQuestPoints = heirCostBuffer;
            float permitPointBuffer = settings.tweak_royalTitleSettings[title.defName].permitPoints;
            listing.AddLabeledSlider(
                $"- 许可点数: {permitPointBuffer.ToString("0")}",
                ref permitPointBuffer,
                0f,
                20f,
                "最小: 0",
                "最大: 20",
                1f
            );
            settings.tweak_royalTitleSettings[title.defName].permitPoints = permitPointBuffer;
            listing.Gap();
            bool dignifiedMeditationBuffer = settings
                .tweak_royalTitleSettings[title.defName]
                .dignifiedMeditation;
            listing.CheckboxEnhanced(
                "- 启用庄严冥想",
                "如果启用，此爵位将为角色激活庄严冥想，允许他们在王座上冥想。",
                ref dignifiedMeditationBuffer
            );
            settings.tweak_royalTitleSettings[title.defName].dignifiedMeditation =
                dignifiedMeditationBuffer;
            bool forceEnableWork = settings.tweak_royalTitleSettings[title.defName].enableWork;
            listing.CheckboxEnhanced("- 无禁用工作", "如果启用，清除爵位中的任何禁用工作。", ref forceEnableWork);
            settings.tweak_royalTitleSettings[title.defName].enableWork = forceEnableWork;
            if (settings.royalTitleSettingsDefaults[title.defName].hasBedroomReqs)
            {
                bool disableBedroomReqs = (bool)
                    settings.tweak_royalTitleSettings[title.defName].disableBedroomRequirements;
                listing.CheckboxEnhanced("- 禁用卧室要求", "如果启用，清除爵位对卧室的任何要求。", ref disableBedroomReqs);
                settings.tweak_royalTitleSettings[title.defName].disableBedroomRequirements =
                    disableBedroomReqs;
            }
            if (settings.royalTitleSettingsDefaults[title.defName].hasThroneroomReqs)
            {
                bool disableThroneroomReqs = (bool)
                    settings.tweak_royalTitleSettings[title.defName].disableThroneroomRequirements;
                listing.CheckboxEnhanced(
                    "- 禁用谒见厅要求",
                    "如果启用，清除爵位对谒见厅的任何要求。",
                    ref disableThroneroomReqs
                );
                settings.tweak_royalTitleSettings[title.defName].disableThroneroomRequirements =
                    disableThroneroomReqs;
            }
            listing.GapLine();
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_RoyaltyPermits), "DoPermitSettings")]
    public class DoPermitSettingsPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing, RoyalTitlePermitDef permit)
        {
            listing.Label(permit.LabelCap);
            listing.GapLine();
            listing.Note($"派系: {permit.faction.LabelCap}", GameFont.Tiny, Color.gray);
            string minTitleBuffer = settings.tweak_royalPermitSettings[permit.defName].minTitle;
            listing.TitleFloatMenu("- 最小头衔", minTitleBuffer, permit);
            settings.tweak_royalPermitSettings[permit.defName].minTitle = minTitleBuffer;
            float permitPointBuffer = settings
                .tweak_royalPermitSettings[permit.defName]
                .permitPointCost;
            listing.AddLabeledSlider(
                $"- 许可点花费：{permitPointBuffer.ToString("0")}",
                ref permitPointBuffer,
                1f,
                20f,
                "最小：1",
                "最大：20",
                1f
            );
            settings.tweak_royalPermitSettings[permit.defName].permitPointCost = permitPointBuffer;
            float cooldownDaysBuffer = settings
                .tweak_royalPermitSettings[permit.defName]
                .cooldownDays;
            listing.AddLabeledSlider(
                $"- 冷却时间：{cooldownDaysBuffer.ToString("0.0")}",
                ref cooldownDaysBuffer,
                0.5f,
                100f,
                "最小：0.5",
                "最大：100",
                0.5f
            );
            settings.tweak_royalPermitSettings[permit.defName].cooldownDays = cooldownDaysBuffer;
            if (permit.royalAid != null)
            {
                float favorCostBuffer = settings
                    .tweak_royalPermitSettings[permit.defName]
                    .favorCost;
                listing.AddLabeledSlider(
                    $"- 皇家声望花费：{favorCostBuffer.ToString("0")}",
                    ref favorCostBuffer,
                    0f,
                    20f,
                    "最小：0",
                    "最大：20",
                    1f
                );
                settings.tweak_royalPermitSettings[permit.defName].favorCost = favorCostBuffer;
                if (permit.royalAid.pawnKindDef != null)
                {
                    float aidPawnCountBuffer = settings
                        .tweak_royalPermitSettings[permit.defName]
                        .pawnCount;
                    listing.AddLabeledSlider(
                        $"- 劳工队人数：{aidPawnCountBuffer.ToString("0")}",
                        ref aidPawnCountBuffer,
                        0f,
                        20f,
                        "最小：0",
                        "最大：20",
                        1f
                    );
                    settings.tweak_royalPermitSettings[permit.defName].pawnCount =
                        aidPawnCountBuffer;
                    float aidDurationBuffer = settings
                        .tweak_royalPermitSettings[permit.defName]
                        .aidDurationDays;
                    listing.AddLabeledSlider(
                        $"- 持续时间：{aidDurationBuffer.ToString("0.0")}",
                        ref aidDurationBuffer,
                        0f,
                        20f,
                        "最小：0",
                        "最大：20",
                        1f
                    );
                    settings.tweak_royalPermitSettings[permit.defName].aidDurationDays =
                        aidDurationBuffer;
                }
            }
            listing.Gap();
            listing.GapLine();
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_GenePacks), "DoSettings_Genepacks")]
    public class DoSettings_GenepacksPatch
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.CheckboxEnhanced(
                "启用基因组调整",
                "这整个部分默认情况下被禁用，主要是为了兼容性，避免与其他选择进行此类调整的模组发生冲突。这些选项允许你选择基因是否可以在基因组中生成。",
                ref settings.tweak_genepackTweaks
            );
            if (settings.tweak_genepackTweaks)
            {
                listing.Note("\b生物科技\b", GameFont.Medium);
                listing.GapLine();
                List<GeneDef> biotechGenes = SettingsPage_GenePacks.GetGenesFromOfficialContent();
                biotechGenes.SortBy(gd => gd.label);
                for (int i = 0; i < biotechGenes.Count(); i++)
                {
                    SettingsPage_GenePacks.DrawGeneSetting(listing, biotechGenes[i]);
                }
                listing.GapLine();
                for (int i = 0; i < SettingsPage_GenePacks.CachedModListing.Count; i++)
                {
                    ModContentPack curMCP = SettingsPage_GenePacks.CachedModListing[i];
                    listing.Note("\b" + curMCP.Name + "\b", GameFont.Medium);
                    listing.GapLine();
                    List<GeneDef> modGenes = SettingsPage_GenePacks.GetGenesFromContentPack(curMCP);
                    modGenes.SortBy(gd => gd.label);
                    for (int j = 0; j < modGenes.Count(); j++)
                    {
                        SettingsPage_GenePacks.DrawGeneSetting(listing, modGenes[j]);
                    }
                    listing.GapLine();
                }
                TweaksGaloreStartup.SetGeneSettingsValues(settings);
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsPage_Defaults), "DoSettings_Defaults")]
    public class DoSettings_DefaultsPatch
    {
        public static Dictionary<string, string> translation = new Dictionary<string, string>()
        {
            { "General", "一般" },
            { "Mechanoids", "机械体" },
            { "Penned_Animals", "围栏动物" },
            { "Power", "电力" },
            { "Raids", "袭击" },
            { "Resources", "资源" },
            { "Royalty", "皇权" },
            { "Royal_Titles", "头衔" },
            { "Royal_Permits", "皇权许可" },
            { "Anima", "仙树" },
            { "Ideology", "文化" },
            { "Gauranlen", "母树" },
            { "Biotech", "生物科技" },
            { "Genepacks", "基因组" },
            { "Polux", "粹树" },
            { "Defaults", "重置" }
        };
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static bool Prefix(Listing_Standard listing)
        {
            listing.Note("这些按钮会重置一类。没有确定键，故按键前请三思。设置需重启已生效。");
            if (listing.ButtonText(translation["General"]))
            {
                DefaultUtil.RestoreSettings_Vanilla(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["General"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreGeneral = true;
            }
            if (listing.ButtonText(translation["Mechanoids"]))
            {
                DefaultUtil.RestoreSettings_Mechanoids(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Mechanoids"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreMechanoids = true;
            }
            if (listing.ButtonText(translation["Penned_Animals"]))
            {
                DefaultUtil.RestoreSettings_PennedAnimals(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Penned_Animals"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restorePennedAnimals = true;
            }
            if (listing.ButtonText(translation["Power"]))
            {
                DefaultUtil.RestoreSettings_Power(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Power"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restorePower = true;
            }
            if (listing.ButtonText(translation["Raids"]))
            {
                DefaultUtil.RestoreSettings_Raids(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Raids"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreRaids = true;
            }
            if (listing.ButtonText(translation["Resources"]))
            {
                DefaultUtil.RestoreSettings_Resources(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Resources"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreResources = true;
            }
            if (listing.ButtonText(translation["Royalty"]))
            {
                DefaultUtil.RestoreSettings_Royalty(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Royalty"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreRoyalty = true;
            }
            if (listing.ButtonText(translation["Royal_Titles"]))
            {
                DefaultUtil.RestoreSettings_RoyalTitles(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Royal_Titles"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreRoyalTitles = true;
            }
            if (listing.ButtonText(translation["Royal_Permits"]))
            {
                DefaultUtil.RestoreSettings_RoyalTitles(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Royal_Permits"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreRoyalPermits = true;
            }
            if (listing.ButtonText(translation["Anima"]))
            {
                DefaultUtil.RestoreSettings_Anima(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Anima"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreAnima = true;
            }
            if (listing.ButtonText(translation["Ideology"]))
            {
                DefaultUtil.RestoreSettings_Ideology(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Ideology"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreIdeology = true;
            }
            if (listing.ButtonText(translation["Gauranlen"]))
            {
                DefaultUtil.RestoreSettings_Gauranlen(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Gauranlen"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreGauranlen = true;
            }
            if (listing.ButtonText(translation["Biotech"]))
            {
                DefaultUtil.RestoreSettings_Biotech(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Biotech"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restoreBiotech = true;
            }
            if (listing.ButtonText(translation["Polux"]))
            {
                DefaultUtil.RestoreSettings_Polux(settings);
                Messages.Message(
                    "Tweaks Galore: {0}设置恢复为默认。".Formatted(translation["Polux"]),
                    MessageTypeDefOf.CautionInput
                );
                TweaksGaloreMod.mod.restorePolux = true;
            }
            if (listing.ButtonText("重置全部"))
            {
                DefaultUtil.RestoreALL(settings);
                Messages.Message("Tweaks Galore: 全部设置恢复为默认。", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreGeneral = true;
                TweaksGaloreMod.mod.restoreMechanoids = true;
                TweaksGaloreMod.mod.restorePennedAnimals = true;
                TweaksGaloreMod.mod.restorePower = true;
                TweaksGaloreMod.mod.restoreRaids = true;
                TweaksGaloreMod.mod.restoreResources = true;
                TweaksGaloreMod.mod.restoreRoyalty = true;
                TweaksGaloreMod.mod.restoreAnima = true;
                TweaksGaloreMod.mod.restoreRoyalTitles = true;
                TweaksGaloreMod.mod.restoreRoyalPermits = true;
                TweaksGaloreMod.mod.restoreIdeology = true;
                TweaksGaloreMod.mod.restoreGauranlen = true;
                TweaksGaloreMod.mod.restoreBiotech = true;
                TweaksGaloreMod.mod.restorePolux = true;
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(SettingsUtil), "SettingsDropdown")]
    public class SettingsDropdownPatch
    {
        public static Dictionary<string, string> translation = new Dictionary<string, string>()
        {
            { "General", "一般" },
            { "Mechanoids", "机械体" },
            { "Penned_Animals", "围栏动物" },
            { "Power", "电力" },
            { "Raids", "袭击" },
            { "Resources", "资源" },
            { "Royalty", "皇权" },
            { "Royal_Titles", "头衔" },
            { "Royal_Permits", "皇权许可" },
            { "Anima", "仙树" },
            { "Ideology", "文化" },
            { "Gauranlen", "母树" },
            { "Biotech", "生物科技" },
            { "Genepacks", "基因组" },
            { "Polux", "粹树" },
            { "Defaults", "重置" }
        };

        public static bool Prefix(
            Listing_Standard listing,
            string name,
            string explanation,
            ref TweaksGaloreSettingsPage value,
            float width
        )
        {
            float curHeight = listing.CurHeight;
            Rect rect = listing.GetRect(Text.LineHeight + listing.verticalSpacing);
            Text.Font = GameFont.Small;
            GUI.color = Color.white;
            TextAnchor anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleLeft;
            Widgets.Label(rect, name);
            Text.Anchor = TextAnchor.MiddleRight;
            Rect rect2 = new Rect(width - 150f, 0f, 150f, 29f);
            if (Widgets.ButtonText(rect2, translation[value.ToString()], true, true, true))
            {
                List<FloatMenuOption> list = new List<FloatMenuOption>();
                List<TweaksGaloreSettingsPage> enumValues = Enum.GetValues(
                        typeof(TweaksGaloreSettingsPage)
                    )
                    .Cast<TweaksGaloreSettingsPage>()
                    .ToList();

                foreach (TweaksGaloreSettingsPage enumValue in enumValues)
                {
                    list.Add(
                        new FloatMenuOption(
                            translation[enumValue.ToString()],
                            delegate()
                            {
                                TweaksGaloreMod.mod.currentPage = enumValue;
                            }
                        )
                    );
                }
                Find.WindowStack.Add(new FloatMenu(list));
            }
            Text.Anchor = anchor;
            Text.Font = GameFont.Tiny;
            listing.ColumnWidth -= 34f;
            GUI.color = Color.gray;
            listing.Label(explanation);
            listing.ColumnWidth += 34f;
            Text.Font = GameFont.Small;
            rect = listing.GetRect(0f);
            rect.height = listing.CurHeight - curHeight;
            rect.y -= rect.height;
            GUI.color = Color.white;
            listing.Gap(6f);
            return false;
        }
    }

    [HarmonyPatch(typeof(TweaksGaloreMod), "DoOptionsCategoryContents")]
    public class DoOptionsCategoryContentsPatch
    {
        public static bool Prefix(Listing_Standard listing)
        {
            var currentPage = TweaksGaloreMod.mod.currentPage;
            listing.SettingsDropdown("当前页面", "", ref currentPage, listing.ColumnWidth);
            listing.Note("对于多数设置，需要重启游戏以生效。", GameFont.Tiny);
            listing.GapLine();
            if (currentPage == TweaksGaloreSettingsPage.General)
            {
                if (TweaksGaloreMod.mod.restoreGeneral)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    SettingsPage_General.DoSettings_Vanilla(listing);
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Mechanoids)
            {
                if (TweaksGaloreMod.mod.restoreMechanoids)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    SettingsPage_Mechanoids.DoSettings_Mechanoids(listing);
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Penned_Animals)
            {
                if (TweaksGaloreMod.mod.restorePennedAnimals)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    SettingsPage_PennedAnimals.DoSettings_PennedAnimals(listing);
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Power)
            {
                if (TweaksGaloreMod.mod.restorePower)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    SettingsPage_Power.DoSettings_Power(listing);
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Raids)
            {
                if (TweaksGaloreMod.mod.restoreRaids)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    SettingsPage_Raids.DoSettings_Raids(listing);
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Resources)
            {
                if (TweaksGaloreMod.mod.restoreResources)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    SettingsPage_Resources.DoSettings_Resources(listing);
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Royalty)
            {
                if (TweaksGaloreMod.mod.restoreRoyalty)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    if (!ModLister.RoyaltyInstalled)
                    {
                        listing.Note("皇权未启用。这些设置无效果。");
                    }
                    else
                    {
                        SettingsPage_Royalty.DoSettings_Royalty(listing);
                    }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Anima)
            {
                if (TweaksGaloreMod.mod.restoreAnima)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    if (!ModLister.RoyaltyInstalled)
                    {
                        listing.Note("皇权未启用。这些设置无效果。");
                    }
                    else
                    {
                        SettingsPage_Anima.DoSettings_Anima(listing);
                    }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Royal_Titles)
            {
                if (TweaksGaloreMod.mod.restoreRoyalTitles)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    if (!ModLister.RoyaltyInstalled)
                    {
                        listing.Note("皇权未启用。这些设置无效果。");
                    }
                    else
                    {
                        SettingsPage_RoyaltyTitles.DoSettings_RoyaltyTitles(listing);
                    }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Royal_Permits)
            {
                if (TweaksGaloreMod.mod.restoreRoyalTitles)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    if (!ModLister.RoyaltyInstalled)
                    {
                        listing.Note("皇权未启用。这些设置无效果。");
                    }
                    else
                    {
                        SettingsPage_RoyaltyPermits.DoSettings_RoyaltyPermits(listing);
                    }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Ideology)
            {
                if (TweaksGaloreMod.mod.restoreIdeology)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    if (!ModLister.IdeologyInstalled)
                    {
                        listing.Note("文化未启用。这些设置无效果。");
                    }
                    else
                    {
                        SettingsPage_Ideology.DoSettings_Ideology(listing);
                    }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Gauranlen)
            {
                if (TweaksGaloreMod.mod.restoreGauranlen)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    if (!ModLister.IdeologyInstalled)
                    {
                        listing.Note("文化未启用。这些设置无效果。");
                    }
                    else
                    {
                        SettingsPage_Gauranlen.DoSettings_Gauranlen(listing);
                    }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Biotech)
            {
                if (TweaksGaloreMod.mod.restoreBiotech)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    if (!ModLister.BiotechInstalled)
                    {
                        listing.Note("生物科技未启用。这些设置无效果。");
                    }
                    else
                    {
                        SettingsPage_Biotech.DoSettings_Biotech(listing);
                    }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Genepacks)
            {
                if (!ModLister.BiotechInstalled)
                {
                    listing.Note("生物科技未启用。这些设置无效果。");
                }
                else
                {
                    SettingsPage_GenePacks.DoSettings_Genepacks(listing);
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Polux)
            {
                if (TweaksGaloreMod.mod.restorePolux)
                {
                    listing.Note("你已将此类标记重置！重启游戏以生效！");
                }
                else
                {
                    if (!ModLister.BiotechInstalled)
                    {
                        listing.Note("生物科技未启用。这些设置无效果。");
                    }
                    else
                    {
                        SettingsPage_Polux.DoSettings_Polux(listing);
                    }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Defaults)
            {
                SettingsPage_Defaults.DoSettings_Defaults(listing);
            }
            return false;
        }
    }
}
