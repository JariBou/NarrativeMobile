namespace _project.Scripts.Interface
{
    public interface IDraggableItemConstraint
    {
        //if false, item can't be placed on DraggableZone
        public bool IsConstraintCompleted();
    }
}
