import { RecurrencesBoardActionsController } from "../../components/recurrences-board/recurrences-board-controller";
import { KeyCodes } from "../../domain/constants/keycodes";

const m_boardActionsController = new RecurrencesBoardActionsController();

/**
 * Listen for the window to get resized
 */
export function listenForWindowResize() 
{
    window.addEventListener('resize', setupBoardActionVisibilities);
}

/**
 * Toggle the action button collapse element
 */
export function setupBoardActionVisibilities() 
{
    if (m_boardActionsController.isCollpaseButtonVisibile()) 
        m_boardActionsController.showCollapseMenu();
    else
        m_boardActionsController.hideCollapseMenu();
}
 
 
 /**
  * Listen for when users hit Ctrl+left/right/down
  * 
  * Ctrl + Left Arrow = jump to previous week
  * Ctrl + Right Arrow = jump to next week
  * Ctrl + Down Arrow = jump to current date
  */
export function listenForArrowKeys() 
{
    document.addEventListener('keydown', function(e) 
    {
        if (!e.ctrlKey) 
        {
            return;
        }
        else if (e.shiftKey || e.altKey) 
        {
            return;
        }
        else if (e.target != document.body) 
        {
            return;
        }
        

        if (e.code == KeyCodes.ARROW_LEFT) 
        {
            m_boardActionsController.jumpToPreviousWeek();
        }
        else if (e.code == KeyCodes.ARROW_RIGHT) 
        {
            m_boardActionsController.jumpToNextWeek();
        }
        else if (e.code == KeyCodes.ARROW_UP) 
        {
            m_boardActionsController.jumpToCurrentDate();
        }
        else 
        {
            return;
        }

        console.log(e);

    });
}