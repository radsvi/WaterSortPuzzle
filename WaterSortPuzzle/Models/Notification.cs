﻿namespace WaterSortPuzzle.Models
{
    public class NotificationDetails
    {
        public NotificationDetails(QuickNotificationOverlay refVisualElement, CancellationTokenSource tokenSource)
        {
            RefVisualElement = refVisualElement;
            TokenSource = tokenSource;
        }
        public CancellationTokenSource TokenSource { get; private set; }
        public QuickNotificationOverlay RefVisualElement { get; private set; }
    }
    public class Notification
    {
        const int closeDelayDefault = 2000; // in ms
        MainWindowVM MainPageVM;
        public StackLayout NotificationBox { get; private set; }
        public Notification(MainWindowVM mainPageVM)
        {
            MainPageVM = mainPageVM;
            //NotificationList = new NotificationsList(MainWindowVM.MainWindow.NotificationBox);
            //NotificationBox = mainPageVM.MainPage.NotificationBox;
        }
        public void Show(string text) => Show(text, MessageType.Information);
        public void Show(string text, int closeDelay = closeDelayDefault) => Show(text, MessageType.Information, closeDelay);
        public void Show(string text, MessageType messageType, int closeDelay = closeDelayDefault)
        { }




        //MainWindowVM MainPageVM;
        //const int closeDelayDefault = 2000; // in ms
        //private bool DisplayDebugMessages { get; } = true;
        ////public CancellationTokenSource TokenSource { get; set; } = null;
        //public StackLayout NotificationBox { get; private set; }
        ////public NotificationsList NotificationList { get; private set; }
        //public Notification(MainWindowVM mainPageVM)
        //{
        //    MainPageVM = mainPageVM;
        //    //NotificationList = new NotificationsList(MainWindowVM.MainWindow.NotificationBox);
        //    NotificationBox = mainPageVM.MainPage.NotificationBox;
        //}
        //public void Show(string text) => Show(text, MessageType.Information);
        //public void Show(string text, int closeDelay = closeDelayDefault) => Show(text, MessageType.Information, closeDelay);
        //public void Show(string text, MessageType messageType, int closeDelay = closeDelayDefault)
        //{
        //    Debug.WriteLine("[Notification: ]" + text);
        //    if (messageType == MessageType.Debug && DisplayDebugMessages is false || messageType == MessageType.Hidden)
        //    {
        //        return;
        //    }

        //    var tokenSource = new CancellationTokenSource();
        //    var notificationControl = new QuickNotificationOverlay(MainPageVM, text, tokenSource);

        //    //NotificationBox.Children.Add(notificationControl);
        //    NotificationBox.Children.Insert(0, notificationControl);

        //    //if (closeDelay != -1)
        //    PopupNotification(notificationControl, closeDelay);
        //}
        //private async void PopupNotification(QuickNotificationOverlay notificationControl, int closeDelay)
        //{
        //    //await Task.Delay(closeDelayDefault, token);

        //    closeDelay = closeDelay / 100;
        //    for (int i = 0; i < closeDelay; i++)
        //    {
        //        await Task.Delay(100);
        //        if (notificationControl.NotificationDetails.TokenSource.Token.IsCancellationRequested)
        //        {
        //            break;
        //        }
        //    }

        //    ClosePopupWindow(notificationControl.NotificationDetails);
        //}
        //public void CloseNotification(object notificationDetailsGenericObject)
        //{
        //    if (notificationDetailsGenericObject.GetType() != typeof(NotificationDetails))
        //    {
        //        return;
        //    }
        //    NotificationDetails notificationDetails = (NotificationDetails)notificationDetailsGenericObject;
        //    notificationDetails.TokenSource.Cancel();
        //}
        //private void ClosePopupWindow(NotificationDetails notificationDetails)
        //{
        //    NotificationBox.Children.Remove(notificationDetails.RefVisualElement);
        //}
    }
}
