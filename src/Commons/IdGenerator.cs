namespace Commons;

using System.Threading;

public class IdGenerator(ulong startingId = 0) {
    private ulong currentId = startingId;

    public ulong NewId => Interlocked.Increment(ref this.currentId);

    public ulong LastId => Interlocked.Read(ref this.currentId);
}
