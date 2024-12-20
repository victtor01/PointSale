type CenterSectionProps = {
  children: React.ReactNode
}

function CenterSection ({ children }: CenterSectionProps) {
  return (
    <section className="flex mx-auto flex-col h-auto w-full max-w-[50rem] p-2">
      {children}
    </section>
  )
}

export { CenterSection }