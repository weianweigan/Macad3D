﻿using Macad.Common;

namespace Macad.Interaction;

public abstract class LiveAction : BaseObject, IMouseEventHandler
{
        public MouseEventData LastMouseEventData { get; protected set; }

        //--------------------------------------------------------------------------------------------------

        public WorkspaceController WorkspaceController
        {
            get { return _WorkspaceController; }
            set
            {
                _WorkspaceController = value;
            }
        }

        //--------------------------------------------------------------------------------------------------

        public object Owner { get; private set; }

        //--------------------------------------------------------------------------------------------------

        public bool IsActive { get; private set; }

        //--------------------------------------------------------------------------------------------------

        WorkspaceController _WorkspaceController;

        //--------------------------------------------------------------------------------------------------

        protected LiveAction(object owner)
        {
            Owner = owner;
        }

        //--------------------------------------------------------------------------------------------------

        public bool Start()
        {
            if (WorkspaceController == null)
                return false;

            OnStart();

            IsActive = true;
            return true;
        }

        //--------------------------------------------------------------------------------------------------

        public void Stop()
        {
            if (!IsActive)
                return;

            OnStop();
            WorkspaceController.HudManager?.SetCursor(null);
            WorkspaceController.RemoveLiveAction(this);
            WorkspaceController = null;
        }

        //--------------------------------------------------------------------------------------------------

        protected virtual void OnStart() {}

        //--------------------------------------------------------------------------------------------------

        // TODO make protected
        public virtual void OnStop() {}

        //--------------------------------------------------------------------------------------------------

        #region IMouseEventHandler

        public virtual bool OnMouseMove(MouseEventData data)
        {
            LastMouseEventData = data;
            return false;
        }

        //--------------------------------------------------------------------------------------------------

        public virtual bool OnMouseDown(MouseEventData data)
        {
            LastMouseEventData = data;
            return false;
        }

        //--------------------------------------------------------------------------------------------------

        public virtual bool OnMouseUp(MouseEventData data)
        {
            LastMouseEventData = data;
            return false;
        }

        //--------------------------------------------------------------------------------------------------

        #endregion
}