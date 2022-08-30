using ChatLineColorMod.Hooks;
using UnityEngine;
using UnityEngine.UI;
using UniverseLib.UI;
using UniverseLib.UI.Models;

namespace ChatLineColorMod.UI.Panels
{
    internal class ClearChatPanel : UniverseLib.UI.Panels.PanelBase
    {

        public static ClearChatPanel Instance { get; private set; }
        public enum VerticalAnchor
        {
            Top,
            Bottom
        }


        public static VerticalAnchor NavbarAnchor = VerticalAnchor.Bottom;

        private static readonly Vector2 NAVBAR_DIMENSIONS = new(1020f, 35f);

        public ClearChatPanel(UIBase owner) : base(owner)
        {
            Instance = this;
        }

        public override string Name => "Clear Chat";

        public override int MinWidth => 90;

        public override int MinHeight => 40;

        public static float PositionX = ((Screen.width * -545) / 2560) - 45;
        public static float PositionY = ((Screen.height * 60) / 1440);

        public override Vector2 DefaultAnchorMin => new Vector2(0.5f, 1f);

        public override Vector2 DefaultAnchorMax => new Vector2(0.5f, 1f);

        public override Vector2 DefaultPosition => new Vector2(PositionX, PositionY);

        public override bool CanDragAndResize => false;

        public static RectTransform NavBarRect { get; private set; }

        public GameObject NavbarTabButtonHolder { get; private set; }
        public ButtonRef clearChatBtn { get; private set; }

        protected override void ConstructPanelContent()
        {

            GameObject navbarPanel = UIFactory.CreateUIObject("MainNavbar", UIRoot);
            UIFactory.SetLayoutGroup<HorizontalLayoutGroup>(navbarPanel, true, true, true, true, 5, 4, 4, 4, 4, TextAnchor.MiddleCenter);
            navbarPanel.AddComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f);
            NavBarRect = navbarPanel.GetComponent<RectTransform>();
            NavBarRect.pivot = new Vector2(0.5f, 1f);

            SetNavBarAnchor();

            // Shop BTN
            clearChatBtn = UIFactory.CreateButton(navbarPanel, "clearChatBtn", "Clear Chat");
            UIFactory.SetLayoutElement(clearChatBtn.Component.gameObject, minWidth: 80, minHeight: 30, preferredWidth: 80, preferredHeight: 30, flexibleWidth: 0, flexibleHeight: 0);
            clearChatBtn.OnClick += ClearChatAction;

        }

        public static void SetNavBarAnchor()
        {
            switch (NavbarAnchor)
            {
                case VerticalAnchor.Top:
                    NavBarRect.anchorMin = new Vector2(0.5f, 1f);
                    NavBarRect.anchorMax = new Vector2(0.5f, 1f);
                    NavBarRect.anchoredPosition = new Vector2(NavBarRect.anchoredPosition.x, 0);
                    NavBarRect.sizeDelta = NAVBAR_DIMENSIONS;
                    break;

                case VerticalAnchor.Bottom:
                    NavBarRect.anchorMin = new Vector2(0.5f, 0f);
                    NavBarRect.anchorMax = new Vector2(0.5f, 0f);
                    NavBarRect.anchoredPosition = new Vector2(NavBarRect.anchoredPosition.x, 200);
                    NavBarRect.sizeDelta = NAVBAR_DIMENSIONS;
                    break;
            }
        }

        public static void ClearChatAction()
        {
            Plugin.Logger.LogInfo("Clear Chat Action");
            HUDChatWindow_Path.clearChat = true;
        }

    }
}
